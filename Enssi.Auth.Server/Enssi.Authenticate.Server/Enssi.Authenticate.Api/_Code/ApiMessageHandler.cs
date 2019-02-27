using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Helpers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using Enssi.Authenticate.Data;

namespace Enssi.Authenticate.Api
{
    public class ApiMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var getToken = default(IEnumerable<string>);
            var getSign = default(IEnumerable<string>);
            var getUserToken = default(IEnumerable<string>);

            request.Headers.TryGetValues("Api.Token", out getToken);
            request.Headers.TryGetValues("Api.Sign", out getSign);
            request.Headers.TryGetValues("Api.UserToken", out getUserToken);

            var token = (getToken ?? new string[0]).FirstOrDefault() ?? "";
            var sign = (getSign ?? new string[0]).FirstOrDefault() ?? "";
            var userToken = (getUserToken ?? new string[0]).FirstOrDefault() ?? "";

            if (token.ToUpper() != Api.Token)
            {
                var task = new Task<HttpResponseMessage>(() => request.CreateErrorResponse(HttpStatusCode.Forbidden, "令牌验证失败。"));
                task.Start();

                return task;
            }

            var querystrings = request.GetQueryNameValuePairs().OrderBy(a => a.Key).ToList();
            var requestBody = request.Content.Headers.ContentType?.MediaType == "application/json" ? request.Content.ReadAsStringAsync().Result : null;
            var querystrs = string.Join("&", querystrings.Select(a => a.Key + "=" + a.Value).ToList());

            var datetimelist = new List<string>()
            {
                DateTime.UtcNow.ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(-1).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(1).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(-2).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(2).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(-3).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(3).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(-4).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(4).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(-5).ToString("yyyyMMddHHmm"),
                DateTime.UtcNow.AddMinutes(5).ToString("yyyyMMddHHmm"),
            };

            var validSign = false;

            foreach (var item in datetimelist)
            {
                var md5 = GetMd5HashStr(item + querystrs + requestBody);
                if (md5.ToUpper() == sign.ToUpper())
                {
                    validSign = true;
                    break;
                }
            }

            if (!validSign)
            {
                var task = new Task<HttpResponseMessage>(() => request.CreateErrorResponse(HttpStatusCode.Forbidden, "签名验证失败。"));
                task.Start();

                return task;
            }

            using (var db = new EnssiAuthenticateEntities())
            {
                // 当前登录用户
                var userTokenModel = db.UserToken.Include("Application").Include("User.UserToRole.Role.Organization").Where(a => a.Token == userToken).FirstOrDefault();
                if (userTokenModel != null && userTokenModel.Application.NameSpace == Api.SystemNameSpace)
                {
                    var context = request.GetRequestContext();
                    context.Principal = new GenericPrincipal(new GenericIdentity(userTokenModel.User.Account), new[] { "User" });
                    HttpContext.Current.Items["User"] = userTokenModel.User;
                }
            }

            return base.SendAsync(request, cancellationToken);
        }

        private IEnumerable<string> GetLocalHostIP()
        {
            var addressList = Dns.GetHostAddresses("localhost");//会返回所有地址，包括IPv4和IPv6   
            foreach (var ip in addressList)
            {
                yield return ip.ToString();
            }
            yield return "192.168.1.22";
        }

        /// <summary>
        /// MD5(32位加密)
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string GetMd5HashStr(string str)
        {
            string pwd = string.Empty;
            //实例化一个md5对像
            var md5 = System.Security.Cryptography.MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}

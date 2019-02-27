using Enssi.Authenticate.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enssi
{
    public class ApiEnssiAuthenticate
    {
       /// <summary>
       /// 秘钥
       /// </summary>
        public const string Token = "37562571C4394A37B18DCE9825644069";
        /// <summary>
        /// 地址
        /// </summary>
        public static string BaseUrl = ApiAutuIniConfig.Read("Network", "ApiEnssiAuthenticateBaseUrl");
        //public static string BaseUrl = IniFile.ReadIniData("Network", "ApiEnssiAuthenticateBaseUrl", ConfigurationManager.AppSettings["ApiEnssiAuthenticateBaseUrl"]);
        /// <summary>
        /// 用户专属秘钥
        /// </summary>
        public static string UserToken { get; set; }
        public static User User { get; set; }

        public async static Task<T> PostAsync<T>(string methodUrl, object obj)
        {
            return await ApiCall.PostAsync<T>(Token, UserToken, BaseUrl + methodUrl, obj);
        }

        public async static Task<T> GetAsync<T>(string methodUrl)
        {
            return await ApiCall.GetAsync<T>(Token, UserToken, BaseUrl + methodUrl);
        }
    }
}

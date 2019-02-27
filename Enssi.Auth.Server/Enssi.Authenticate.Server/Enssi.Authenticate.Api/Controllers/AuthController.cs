using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Http;

namespace Enssi.Authenticate.Api.Controllers
{
    public class AuthController : ApiController
    {
        private EnssiAuthenticateEntities db => Api.DbEnssiAuthenticate;

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [ActionName("CX00101")]
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<Auth_Login_Result> Login(string SystemNameSpace, string UsingNameSpace, string Account, string Password)
        {
            var application = db.Application.Where(a => a.NameSpace == SystemNameSpace).FirstOrDefault();
            var usingApplication = db.Application.Where(a => a.NameSpace == UsingNameSpace).FirstOrDefault();

            if (application == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "系统命名空间不存在。参数名：SystemNameSpace。" };
            }
            if (usingApplication == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "使用命名空间不存在。参数名：UsingNameSpace。" };
            }
            if (string.IsNullOrWhiteSpace(Account))
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "账号不能为空。" };
            }
            if (string.IsNullOrEmpty(Password))
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "密码不能为空。" };
            }

            var user = db.User.Where(a => a.Account == Account && a.Password == Password && a.IsDeleted == 0).FirstOrDefault();
            if (user == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "用户名密码错误。" };
            }

            string token = Guid.NewGuid().ToString("N").ToUpper();
            var tokenModel = db.UserToken.Where(a => a.UserID == user.ID && a.ApplicationID == application.ID && a.UsingApplicationID == usingApplication.ID).FirstOrDefault();
            if (tokenModel != null)
            {
                token = tokenModel.Token;
            }

            db.UserToken.Where(a => a.UserID == user.ID && a.ApplicationID == application.ID && a.UsingApplicationID == usingApplication.ID).ToList().ForEach(a => db.UserToken.Remove(a));

            var usertoken = new UserToken
            {
                ID = Guid.NewGuid(),
                ApplicationID = application.ID,
                UsingApplicationID = usingApplication.ID,
                UserID = user.ID,
                Token = token,
                UpdateTime = DateTime.Now
            };
            db.UserToken.Add(usertoken);
            db.SaveChanges();

            return new ApiResult<Auth_Login_Result> { ErrorCode = 0, Data = new Auth_Login_Result { Account = user.Account, UserID = user.ID, UserName = user.Name, UserToken = usertoken.Token } };
        }

        /// <summary>
        /// 获取全部指纹
        /// </summary>
        /// <returns></returns>
        [ActionName("CX00201")]
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<List<FingerprintModel>> GetFingerprint()
        {
            var users = db.User.Where(o => o.IsDeleted == 0).ToList();
            List<FingerprintModel> list = new List<FingerprintModel>();
            int i = 1;
            foreach (var item in users)
            {
                if (item.Fingerprint != null)
                {
                    FingerprintModel model = new FingerprintModel()
                    {
                        FID = i,
                        GID = item.ID,
                        SID = item.Account,
                        SValue = item.Password,
                        FingerValue = item.Fingerprint
                    };
                    list.Add(model);
                    i++;
                }
                if (item.FingerprintSpare != null)
                {
                    FingerprintModel model = new FingerprintModel()
                    {
                        FID = i,
                        GID = item.ID,
                        SID = item.Account,
                        SValue = item.Password,
                        FingerValue = item.FingerprintSpare
                    };
                    list.Add(model);
                    i++;
                }
            }

            return new ApiResult<List<FingerprintModel>> { ErrorCode = 0, Data = list };
        }

        /// <summary>
        /// 指纹登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="UID">用户编号</param>
        /// <returns></returns>
        [ActionName("CX00202")]
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<Auth_Login_Result> LoginByFinger(string SystemNameSpace, string UsingNameSpace, Guid UID)
        {
            var application = db.Application.Where(a => a.NameSpace == SystemNameSpace).FirstOrDefault();
            var usingApplication = db.Application.Where(a => a.NameSpace == UsingNameSpace).FirstOrDefault();

            if (application == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "系统命名空间不存在。参数名：SystemNameSpace。" };
            }
            if (usingApplication == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "使用命名空间不存在。参数名：UsingNameSpace。" };
            }
            if (UID == Guid.Empty)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "登录指纹不能为空" };
            }
            var user = db.User.Where(a => a.ID == UID && a.IsDeleted == 0).FirstOrDefault();
            if (user == null)
            {
                return new ApiResult<Auth_Login_Result> { ErrorCode = 1, Message = "用户不存在" };
            }

            string token = Guid.NewGuid().ToString("N").ToUpper();
            var tokenModel = db.UserToken.Where(a => a.UserID == UID && a.ApplicationID == application.ID && a.UsingApplicationID == usingApplication.ID).FirstOrDefault();
            if (tokenModel != null)
            {
                token = tokenModel.Token;
            }

            db.UserToken.Where(a => a.UserID == UID && a.ApplicationID == application.ID && a.UsingApplicationID == usingApplication.ID).ToList().ForEach(a => db.UserToken.Remove(a));

            var usertoken = new UserToken
            {
                ID = Guid.NewGuid(),
                ApplicationID = application.ID,
                UsingApplicationID = usingApplication.ID,
                UserID = UID,
                Token = token,
                UpdateTime = DateTime.Now
            };
            db.UserToken.Add(usertoken);
            db.SaveChanges();

            return new ApiResult<Auth_Login_Result> { ErrorCode = 0, Data = new Auth_Login_Result { Account = user.Account, UserID = UID, UserName = user.Name, UserToken = usertoken.Token } };
        }

        /// <summary>
        /// 扫脸登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="OrgID">组织架构编号</param>
        /// <param name="KeyCode">扫脸码</param>
        /// <param name="Application">应用编码</param>
        /// <returns></returns>
        [ActionName("CX00301")]
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<Auth_Login_Result> LoginScanningFace(string SystemNameSpace, string UsingNameSpace, Guid OrgID, string KeyCode, string Application)
        {
            return new ApiResult<Auth_Login_Result>();
        }

        /// <summary>
        /// 客户端权限验证，获得当前用户权限按钮验证
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <returns></returns>
        [ActionName("CX00401")]
        [HttpGet]
        public ApiResult<List<PermissionBtnUser>> GetCurrentUserPermissionBtnAll(string SystemNameSpace)
        {
            var roleids = Api.User.UserToRole.Select(a => a.RoleID).ToList();
            var list = db.PermissionBtn.Where(a => a.Application.NameSpace == SystemNameSpace).Select(a => new PermissionBtnUser { ButtonName = a.ButtonName, FormName = a.FormName, NoPermissionType = a.NoPermissionType, HasPermission = a.PermissionBtnRole.Any(b => roleids.Contains(b.RoleID)) }).ToList();
            return new ApiResult<List<PermissionBtnUser>> { Data = list };
        }
    }
}

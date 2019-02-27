using Enssi.Authenticate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enssi.Authenticate.Api
{
    public static class Api
    {
        /// <summary>
        /// 系统命名空间
        /// </summary>
        public const string SystemNameSpace = "Enssi.Authenticate";
        /// <summary>
        /// 系统令牌
        /// </summary>
        public const string Token = "37562571C4394A37B18DCE9825644069";
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static User User => (User)HttpContext.Current.Items["User"];
        /// <summary>
        /// EnssiAuthenticate 数据库访问对象
        /// </summary>
        public static EnssiAuthenticateEntities DbEnssiAuthenticate => (EnssiAuthenticateEntities)(HttpContext.Current.Items["EnssiAuthenticateEntities"] = HttpContext.Current.Items["EnssiAuthenticateEntities"] ?? new EnssiAuthenticateEntities());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enssi.Authenticate.Model
{
    public class PermissionBtnUser
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// 窗体名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 无权限时操作方式：禁用，隐藏
        /// </summary>
        public string NoPermissionType { get; set; }
        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public bool HasPermission { get; set; }
    }
}

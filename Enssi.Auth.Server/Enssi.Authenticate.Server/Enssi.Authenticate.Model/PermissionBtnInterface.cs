//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enssi.Authenticate.Data
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// 权限按钮权限功能关系表
    /// </summary>
    public partial class PermissionBtnInterface
    {
    	public System.Guid ID { get; set; }
    	/// <summary>
    	/// 权限按钮Id
    	/// </summary>
    	public System.Guid PermissionButtonID { get; set; }
    	/// <summary>
    	/// 权限功能Id
    	/// </summary>
    	public System.Guid PermissionInterfaceID { get; set; }
    
        public virtual PermissionBtn PermissionBtn { get; set; }
        public virtual PermissionInterface PermissionInterface { get; set; }
    }
}
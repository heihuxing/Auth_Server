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
    /// 接口角色关系表
    /// </summary>
    public partial class PermissionInterfaceRole
    {
    	public System.Guid ID { get; set; }
    	/// <summary>
    	/// 功能Id
    	/// </summary>
    	public System.Guid PermissionInterfaceID { get; set; }
    	/// <summary>
    	/// 角色Id
    	/// </summary>
    	public System.Guid RoleID { get; set; }
    
        public virtual PermissionInterface PermissionInterface { get; set; }
        public virtual Role Role { get; set; }
    }
}

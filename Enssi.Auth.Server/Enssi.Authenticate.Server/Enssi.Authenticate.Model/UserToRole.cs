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
    /// 用户角色关系表
    /// </summary>
    public partial class UserToRole
    {
    	public System.Guid ID { get; set; }
    	/// <summary>
    	/// 用户Id
    	/// </summary>
    	public System.Guid UserID { get; set; }
    	/// <summary>
    	/// 角色Id
    	/// </summary>
    	public System.Guid RoleID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
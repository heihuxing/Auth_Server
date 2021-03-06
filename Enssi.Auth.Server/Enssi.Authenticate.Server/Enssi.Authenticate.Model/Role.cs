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
    /// 角色表
    /// </summary>
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            this.ApproveTemplateDetail = new HashSet<ApproveTemplateDetail>();
            this.PermissionBtnRole = new HashSet<PermissionBtnRole>();
            this.PermissionInterfaceRole = new HashSet<PermissionInterfaceRole>();
            this.UserToRole = new HashSet<UserToRole>();
        }
    
    	public System.Guid ID { get; set; }
    	/// <summary>
    	/// 角色名称
    	/// </summary>
    	public string Name { get; set; }
    	/// <summary>
    	/// 角色的说明
    	/// </summary>
    	public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApproveTemplateDetail> ApproveTemplateDetail { get; set; }
        public virtual Organization Organization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionBtnRole> PermissionBtnRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionInterfaceRole> PermissionInterfaceRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserToRole> UserToRole { get; set; }
    }
}

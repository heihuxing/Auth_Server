using System;
using System.Collections.Generic;
using System.Linq;
using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;

namespace Enssi.Authenticate.Client
{
    /// <summary>
    /// Login 账号登录
    /// </summary>
    public class Auth_Login_Request
    {
        /// <summary>
        /// 系统命名空间
        /// </summary>
        public string SystemNameSpace { get; set; }
        /// <summary>
        /// 使用中系统命名空间
        /// </summary>
        public string UsingNameSpace { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// LoginFingerprint 指纹登录
    /// </summary>
    public class Auth_LoginFingerprint_Request
    {
        /// <summary>
        /// 指纹码
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 应用编码
        /// </summary>
        public string Application { get; set; }
    }

    /// <summary>
    /// LoginScanning 扫脸登录
    /// </summary>
    public class Auth_LoginScanning_Request
    {
        /// <summary>
        /// 扫脸码
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 应用编码
        /// </summary>
        public string Application { get; set; }
    }

    /// <summary>
    /// DelOrganization 删除组织架构
    /// </summary>
    public class Permission_DelOrganization_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetOrganization 获取组织架构
    /// </summary>
    public class Permission_GetOrganization_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetOrganizationAll 获得全部组织架构
    /// </summary>
    public class Permission_GetOrganizationAll_Request
    {
        /// <summary>
        /// 类型:Company公司;Dept部门; Position岗位
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetOrganizationListByPID 根据父级ID获得组织架构列表
    /// </summary>
    public class Permission_GetOrganizationListByPID_Request
    {
        public Guid? PID { get; set; }
    }

    /// <summary>
    /// 获取通用组织
    /// </summary>
    public class Permission_GetOrganizationGeneral_Request
    {
        public Guid? id { get; set; }
    }

    /// <summary>
    /// 删除通用组织
    /// </summary>
    public class Permission_DelOrganizationGeneral_Request
    {
        public Guid id { get; set; }
    }

    /// <summary>
    /// 获取全部通用组织
    /// </summary>
    public class Permission_GetOrganizationGeneralAll_Request
    {
        public string Name { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelApplication 删除应用
    /// </summary>
    public class Permission_DelApplication_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetApplication 获取应用
    /// </summary>
    public class Permission_GetApplication_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetApplicationAll 获得全部应用
    /// </summary>
    public class Permission_GetApplicationAll_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Space { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelPermissionGroup 删除窗体
    /// </summary>
    public class Permission_DelPermissionGroup_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetPermissionGroup 获取窗体
    /// </summary>
    public class Permission_GetPermissionGroup_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// P表示Pid查询,I表示ID查询
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// GetPermissionGroupName 根据名称获取窗体
    /// </summary>
    public class Permission_GetPermissionGroupName_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// GetPermissionGroupList 获得全部窗体
    /// </summary>
    public class Permission_GetPermissionGroupList_Request
    {
        public Guid ApplicationID { get; set; }
    }

    /// <summary>
    /// GetPermissionGroupAll 获得全部窗体
    /// </summary>
    public class Permission_GetPermissionGroupAll_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelPermissionBtn 删除窗体对应的按钮
    /// </summary>
    public class Permission_DelPermissionBtn_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetPermissionBtn 获取窗体对应的按钮
    /// </summary>
    public class Permission_GetPermissionBtn_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetPermissionBtnAll 获得全部窗体对应的按钮
    /// </summary>
    public class Permission_GetPermissionBtnAll_Request
    {
        /// <summary>
        /// 按钮名
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// 窗体名
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid ApplicationID { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelPermissionInterface 删除权限接口
    /// </summary>
    public class Permission_DelPermissionInterface_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetPermissionInterface 获取权限接口
    /// </summary>
    public class Permission_GetPermissionInterface_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetPermissionInterfaceAll 获得全部权限接口
    /// </summary>
    public class Permission_GetPermissionInterfaceAll_Request
    {
        /// <summary>
        /// 接口名
        /// </summary>
        public string InterfaceName { get; set; }
        /// <summary>
        /// 权限组ID
        /// </summary>
        public Guid? PermissionGroupID { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid? ApplicationID { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelUserToRole 删除用户角色关系
    /// </summary>
    public class Permission_DelUserToRole_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetUserToRole 获取用户角色关系
    /// </summary>
    public class Permission_GetUserToRole_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetUserToRoleAll 获得全部用户角色关系
    /// </summary>
    public class Permission_GetUserToRoleAll_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// EditPermissionBtnRole 添加/修改用户角色关系
    /// </summary>
    public class Permission_EditPermissionBtnRole_Request
    {
        public Guid RoleID { get; set; }
    }

    /// <summary>
    /// GetPermissionBtnRole 获取用户角色关系
    /// </summary>
    public class Permission_GetPermissionBtnRole_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetPermissionBtnRoleAll 获得全部用户角色关系
    /// </summary>
    public class Permission_GetPermissionBtnRoleAll_Request
    {
        /// <summary>
        /// RoleID
        /// </summary>
        public Guid RoleID { get; set; }
    }
    /// <summary>
    /// DelCommonWords 删除消息返回码
    /// </summary>
    public class Public_DelCommonWords_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetCommonWords 获取消息返回码
    /// </summary>
    public class Public_GetCommonWords_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetCommonWordsAll 获得全部消息返回码
    /// </summary>
    public class Public_GetCommonWordsAll_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DelInterface 删除接口
    /// </summary>
    public class Public_DelInterface_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetInterface 获取接口
    /// </summary>
    public class Public_GetInterface_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetInterface 获取接口
    /// </summary>
    public class Public_GetInterfaceDetail_Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetInterfaceAll  获得全部接口
    /// </summary>
    public class Public_GetInterfaceAll_Request
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string InterfaceNo { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetInterfaceHisAll 获得全部接口历史
    /// </summary>
    public class Public_GetInterfaceHisAll_Request
    {
        /// <summary>
        /// 接口ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetInterfaceHisAll 获得全部接口历史
    /// </summary>
    public class Public_GetInterfaceDetaiHisAll_Request
    {
        /// <summary>
        /// 接口ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }
    /// <summary>
    /// GetUserInfoAll 获取全部用户
    /// </summary>
    public class User_GetUserInfoAll_Request
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间1
        /// </summary>
        public DateTime? CreateTime1 { get; set; }
        /// <summary>
        /// 创建时间2
        /// </summary>
        public DateTime? CreateTime2 { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetUserByOrgID 根据组织架构编号获得用户
    /// </summary>
    public class User_GetUserByOrgID_Request
    {
        /// <summary>
        /// 组织架构编号
        /// </summary>
        public Guid OrgID { get; set; }
    }

    /// <summary>
    /// GetUserInfo 获取用户
    /// </summary>
    public class User_GetUserInfo_Request
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? Id { get; set; }
    }

    /// <summary>
    /// DelUserInfo 删除用户
    /// </summary>
    public class User_DelUserInfo_Request
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetSystem 根据用户编号获取其有哪些系统
    /// </summary>
    public class User_GetSystem_Request
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get; set; }
    }

    /// <summary>
    /// GetUserPermission根据用户编号+具体系统获取对应功能权限
    /// </summary>
    public class User_GetUserPermission_Request
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Application { get; set; }
    }

    /// <summary>
    /// DelRole 删除角色
    /// </summary>
    public class User_DelRole_Request
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid id { get; set; }
    }

    /// <summary>
    /// GetRole 获取角色
    /// </summary>
    public class User_GetRole_Request
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid? id { get; set; }
    }

    /// <summary>
    /// GetRoleAll 获得全部角色
    /// </summary>
    public class User_GetRoleAll_Request
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetRoleToUser 查询此角色有多少用户
    /// </summary>
    public class User_GetRoleToUser_Request
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetUserToRole 根据用户查询用户有多少个角色
    /// </summary>
    public class User_GetUserToRole_Request
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}

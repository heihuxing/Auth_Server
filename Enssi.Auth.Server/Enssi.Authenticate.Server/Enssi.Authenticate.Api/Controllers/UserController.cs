using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Enssi.Authenticate.Api.Controllers
{
    public class UserController : ApiController
    {
        private EnssiAuthenticateEntities db => Api.DbEnssiAuthenticate;

        #region 用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="Id">用户编号</param>
        /// <returns></returns>
        [ActionName("CX00201")]
        [HttpGet]
        public ApiResult<User> GetUserInfo(Guid Id)
        {
            var Model = db.User.Include("UserToRole.Role.Organization").Where(a => a.ID == Id).FirstOrDefault();
            return new ApiResult<User> { Data = Model };
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Name">姓名</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00202")]
        [HttpGet]
        public ApiResult<PagedList<User>> GetUserInfoAll(string Account, string Name, int PageIndex, int PageSize)
        {
            Account = Account ?? "";
            var List = db.User.Where(o => o.Account.Contains(Account)).AsQueryable();
            Name = Name ?? "";
            if (!string.IsNullOrWhiteSpace(Name))
            {
                List = List.Where(o => o.Name.Contains(Name));
            }
            List = List.OrderByDescending(a => a.CreateTime);
            return new ApiResult<PagedList<User>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 根据组织架构编号获得用户
        /// </summary>
        /// <param name="OrgID">组织架构编号</param>
        /// <returns></returns>
        [ActionName("CX00203")]
        [HttpGet]
        public ApiResult<List<User>> GetUserByOrgID(Guid OrgID)
        {
            var list = db.User.Where(a => a.UserToRole.Any(b => b.RoleID == OrgID)).OrderBy(a => a.Name).ToList();
            return new ApiResult<List<Data.User>> { Data = list };
        }

        /// <summary>
        /// 根据门诊编号获得用户
        /// </summary>
        /// <param name="CID">门诊编号</param>
        /// <returns></returns>
        [ActionName("CX00204")]
        [HttpGet]
        public ApiResult<IQueryable<V_UserToRoleQuery>> GetUserByCID(string CID)
        {
            var Data = db.V_UserToRoleQuery.AsQueryable().Where(a => a.IdPath.Contains(CID)).OrderBy(i => i.UserName);

            return new ApiResult<IQueryable<V_UserToRoleQuery>> { Data = Data };
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="Account">用户账号</param>
        /// <returns></returns>
        [ActionName("CX00206")]
        [HttpGet]
        public ApiResult<User> GetUserInfo(string Account)
        {
            var Model = db.User.Where(a => a.Account == Account).FirstOrDefault();
            return new ApiResult<User> { Data = Model };
        }

        /// <summary>
        /// 添加/修改用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        [ActionName("XG00201")]
        [HttpPost]
        public ApiResult EditUserInfo(User model)
        {
            var ID = model.ID;
            if (ID != Guid.Empty)
            {
                var Ids = model.UserToRole.Where(a => a.ID != Guid.Empty).Select(a => a.ID).ToList();

                db.UserToRole.RemoveRange(db.UserToRole.Where(a => a.UserID == model.ID && !Ids.Contains(a.ID)).ToList());

                model.UserToRole.Where(a => a.ID != Guid.Empty).ToList().ForEach(a => model.UserToRole.Remove(a));

                foreach (var item in model.UserToRole)
                {
                    item.ID = Guid.NewGuid();
                    item.Role = null;
                    item.User = null;
                }

                db.User.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = EntityState.Modified;
                foreach (var item in new[] { "CreateTime" })
                {
                    Entry.Property(item).IsModified = false;
                }
                foreach (var item in model.UserToRole)
                {
                    var itemEntry = db.Entry(item);
                    itemEntry.State = EntityState.Added;
                }
            }
            else
            {
                foreach (var item in model.UserToRole)
                {
                    item.ID = Guid.NewGuid();
                    item.Role = null;
                    item.User = null;
                }
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                db.User.Add(model);
            }

            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 启用停用
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="tag">状态（0：启用；1：停用）</param>
        /// <returns></returns>
        [ActionName("SC00201")]
        [HttpPost]
        public ApiResult DelUserInfo(Guid id, int tag)
        {
            var user = db.User.Where(o => o.ID == id).FirstOrDefault();
            user.IsDeleted = tag;
            db.SaveChanges();
            return new ApiResult { Message = "成功" };
        }

        /// <summary>
        /// 根据用户编号+具体系统获取对应功能权限
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="Application">应用名称</param>
        /// <returns>用户</returns>
        [ActionName("CX00205")]
        [HttpGet]
        public ApiResult<IQueryable<V_UserPermissionQuery>> GetUserPermission(string UserID, string Application)
        {
            var Data = db.V_UserPermissionQuery.AsQueryable();
            if (!string.IsNullOrEmpty(UserID))
            {
                Data = Data.Where(a => a.UserID.ToString() == UserID);
            }
            if (!string.IsNullOrEmpty(Application))
            {
                Data = Data.Where(a => a.AppName == Application);
            }
            return new ApiResult<IQueryable<V_UserPermissionQuery>> { Data = Data };
        }

        #endregion

        #region 角色

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns></returns>
        [ActionName("XZ00301")]
        [HttpPost]
        public ApiResult AddRole(Role model)
        {

            db.Role.Add(model);
            db.SaveChanges();
            return new ApiResult { Message = "添加成功！" };
        }

        /// <summary>
        /// 添加/修改角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns></returns>
        [ActionName("XG00301")]
        [HttpPost]
        public ApiResult EditRole(Role model)
        {
            var ID = model.ID;
            if (ID != Guid.Empty)
            {
                db.Role.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = EntityState.Modified;
            }
            else
            {
                model.ID = Guid.NewGuid();
                db.Role.Add(model);
            }

            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
        [ActionName("SC00301")]
        [HttpPost]
        public ApiResult DelRole(Guid id)
        {
            var Role = new Role() { ID = id };
            db.Role.Attach(Role);
            db.Role.Remove(Role);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
        [ActionName("CX00301")]
        [HttpGet]
        public ApiResult<Role> GetRole(Guid? id)
        {
            var Model = db.Role.Include("Organization").Where(a => a.ID == id).FirstOrDefault();
            return new ApiResult<Role> { Data = Model };
        }

        /// <summary>
        /// 获得全部角色
        /// </summary>
        /// <param name="Name">角色名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00302")]
        [HttpGet]
        public ApiResult<PagedList<Role>> GetRoleAll(string Name, int PageIndex, int PageSize)
        {
            var List = db.Role.Where(a => a.Organization == null).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                List = List.Where(a => a.Name.Contains(Name));
            }
            return new ApiResult<PagedList<Role>> { Data = List.OrderByDescending(a => a.Name).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 查询此角色有多少用户
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00303")]
        [HttpGet]

        public ApiResult<PagedList<V_UserToRoleQuery>> GetRoleToUser(Guid id, int PageIndex, int PageSize)
        {
            var UserToRoleSel = db.V_UserToRoleQuery.Where(a => a.RoleID == id).ToList();
            return new ApiResult<PagedList<V_UserToRoleQuery>> { Data = UserToRoleSel.AsQueryable().OrderByDescending(a => a.ID).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 根据用户查询用户有多少个角色
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00304")]
        [HttpGet]
        public ApiResult<PagedList<V_UserToRoleQuery>> GetUserToRole(Guid id, int PageIndex, int PageSize)
        {
            var UserToRoleSel = db.V_UserToRoleQuery.Where(a => a.UserID == id).ToList();
            return new ApiResult<PagedList<V_UserToRoleQuery>> { Data = UserToRoleSel.AsQueryable().OrderByDescending(a => a.ID).ToPagedList(PageIndex, PageSize) };
        }

        #endregion

    }
}

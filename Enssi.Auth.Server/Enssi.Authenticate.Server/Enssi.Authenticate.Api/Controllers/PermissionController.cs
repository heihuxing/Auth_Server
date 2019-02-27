using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Enssi.Authenticate.Api.Controllers
{
    public class PermissionController : ApiController
    {
        private EnssiAuthenticateEntities db => Api.DbEnssiAuthenticate;

        #region 组织架构

        /// <summary>
        /// 添加/修改组织架构
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00401")]
        [HttpPost]
        public ApiResult EditOrganization(Organization model)
        {
            var ID = model.ID;
            if (ID != Guid.Empty)
            {
                var oldpath = db.Organization.Where(a => a.ID == ID).Select(a => new { a.IdPath, a.Path }).FirstOrDefault();
                var listPath = db.Organization.Where(a => a.Path.StartsWith(oldpath.Path) && a.ID != ID && a.ParentID != model.ParentID).ToList();
                foreach (var item in listPath)
                {
                    if (item.Path != oldpath.Path)
                    {
                        item.Path = model.Path + item.Path.Substring(oldpath.Path.Length);
                    }
                }
                var listIdPath = db.Organization.Where(a => a.Path.StartsWith(oldpath.IdPath) && a.ID != ID && a.ParentID != model.ParentID).ToList();
                foreach (var item in listIdPath)
                {
                    if (item.IdPath != oldpath.IdPath)
                    {
                        item.IdPath = model.IdPath + item.IdPath.Substring(oldpath.IdPath.Length);
                    }
                }

                db.Organization.Attach(model);

                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;

                var roleEntry = db.Entry(model.Role);
                roleEntry.State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                model.ID = Guid.NewGuid();
                model.IdPath.Replace(Guid.Empty.ToString(), model.ID.ToString());
                db.Organization.Add(model);
            }
            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除组织架构
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00401")]
        [HttpPost]
        public ApiResult DelOrganization(Guid id)
        {
            var Organization = new Organization() { ID = id };
            db.Organization.Attach(Organization);
            db.Organization.Remove(Organization);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取组织架构
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00401")]
        [HttpGet]
        public ApiResult<Organization> GetOrganization(Guid? id)
        {
            var Model = db.Organization.Include("Organization2").Include("Role").Where(p => p.ID == id).FirstOrDefault();
            return new ApiResult<Organization> { Data = Model };
        }

        /// <summary>
        /// 获得全部组织架构
        /// </summary>
        /// <param name="PredicateOrganization">查询条件</param>
        /// <param name="OrderByOrganization">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00402")]
        [HttpGet]
        public ApiResult<PagedList<Organization>> GetOrganizationAll(string PredicateOrganization, string OrderByOrganization, string Includes, int PageIndex, int PageSize)
        {
            var result = GetOrganizationAllSelector(PredicateOrganization, OrderByOrganization, string.IsNullOrEmpty(Includes) ? null : "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<Organization>> { Data = result.Data.CastPagedList<Organization>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获得全部组织架构
        /// </summary>
        /// <param name="PredicateOrganization">查询条件</param>
        /// <param name="OrderByOrganization">排序条件</param>
        /// <param name="SelectorOrganization">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00403")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetOrganizationAllSelector(string PredicateOrganization, string OrderByOrganization, string SelectorOrganization, int PageIndex, int PageSize)
        {
            var list = db.Organization.Includes(Utility.GetIncludes(SelectorOrganization));

            var selector = Utility.DeserializeSelector<Organization>(SelectorOrganization);
            var predicateList = Utility.DeserializePredicate<Organization>(PredicateOrganization);
            foreach (var item in predicateList)
            {
                list = list.Where(item);
            }

            var orderByList = Utility.DeserializeOrderBy<Organization>(OrderByOrganization);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => a.Sort).ThenBy(a => a.Name);
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }


        /// <summary>
        /// 获得全部组织架构
        /// </summary>
        /// <returns></returns>
        [ActionName("CX00405")]
        [HttpGet]
        public ApiResult<List<Organization>> GetOrganizationList()
        {
            var List = db.Organization.OrderBy(a => a.Sort).ThenBy(a => a.Name).ToList();
            return new ApiResult<List<Organization>> { Data = List.ToList() };
        }

        /// <summary>
        /// 根据父级ID获得组织架构列表
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        [ActionName("CX00406")]
        [HttpGet]
        public ApiResult<List<Organization>> GetOrganizationListByPID(Guid? PID)
        {
            var list = db.Organization.Where(a => a.ParentID == PID).OrderBy(a => a.Sort).ThenBy(a => a.Name).ToList();
            return new ApiResult<List<Organization>> { Data = list };
        }

        /// <summary>
        /// 获取通用组织下通用名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// [ActionName("")]
        [HttpGet]
        public ApiResult<List<OrganizationGeneral>> GetOrganizationGeneralByType(string type)
        {
            var list = db.OrganizationGeneral.Where(a => a.Type == type).ToList();
            return new ApiResult<List<OrganizationGeneral>> { Data = list };
        }
        #endregion 通用组织管理
        /// <summary>
        /// 添加修改通用组织
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>

        [HttpPost]
        public ApiResult EditOrganizationGeneral(OrganizationGeneral model)
        {
            var ID = model.ID;
            if (ID != Guid.Empty)
            {
                db.OrganizationGeneral.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                model.ID = Guid.NewGuid();
                db.OrganizationGeneral.Add(model);
            }
            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }
        /// <summary>
        /// 获取通用组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public ApiResult<OrganizationGeneral> GetOrganizationGeneral(Guid? id)
        {
            var Model = db.OrganizationGeneral.Where(a => a.ID == id.Value).FirstOrDefault();
            return new ApiResult<OrganizationGeneral> { Data = Model };
        }
        /// <summary>
        /// 删除通用组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public ApiResult DelOrganizationGeneral(Guid id)
        {
            var List = db.OrganizationGeneral.Where(a => a.ID == id).FirstOrDefault();
            db.OrganizationGeneral.Remove(List);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }
        /// <summary>
        /// 获取全部通用组织
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>

        [HttpGet]
        public ApiResult<PagedList<OrganizationGeneral>> GetOrganizationGeneralAll(string Type, string Name, int PageIndex, int PageSize)
        {
            var List = db.OrganizationGeneral.AsQueryable();
            if (!string.IsNullOrEmpty(Type))
            {
                List = List.Where(a => a.Type == Type);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                List = List.Where(a => a.Name.Contains(Name));
            }
            return new ApiResult<PagedList<OrganizationGeneral>> { Data = List.OrderBy(a => a.ID).ThenBy(a => a.Name).ToPagedList(PageIndex, PageSize) };
        }
        #region

        #endregion

        #region 应用

        /// <summary>
        /// 添加/修改应用
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00501")]
        [HttpPost]
        public ApiResult EditApplication(Application model)
        {
            var ID = model.ID;
            if (ID != Guid.Empty)
            {
                db.Application.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                model.ID = Guid.NewGuid();
                db.Application.Add(model);
            }
            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00501")]
        [HttpPost]
        public ApiResult DelApplication(Guid id)
        {
            var List = db.Application.Where(a => a.ID == id).FirstOrDefault();
            db.Application.Remove(List);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00501")]
        [HttpGet]
        public ApiResult<Application> GetApplication(Guid? id)
        {
            var Modle = db.Application.Where(a => a.ID == id).FirstOrDefault();
            return new ApiResult<Application> { Data = Modle };
        }

        /// <summary>
        /// 获得全部应用
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Space">命名空间</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00502")]
        [HttpGet]
        public ApiResult<PagedList<Application>> GetApplicationAll(string Name, string Space, int PageIndex, int PageSize)
        {
            var List = db.Application.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                List = List.Where(a => a.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Space))
            {
                List = List.Where(a => a.NameSpace.Contains(Space));
            }
            return new ApiResult<PagedList<Application>> { Data = List.OrderByDescending(a => a.Name).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获得全部应用
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        [ActionName("CX00503")]
        [HttpGet]
        public ApiResult<List<Application>> GetApplicationAllOrderByRolePermissionBtn(Guid? RoleID)
        {
            var List = db.Application.AsQueryable();
            return new ApiResult<List<Application>> { Data = List.OrderByDescending(a => a.PermissionBtn.Count(b => b.PermissionBtnRole.Any(c => c.RoleID == RoleID))).ThenByDescending(a => a.Name).ToList() };
        }

        #endregion

        #region 窗体/功能/报表

        /// <summary>
        /// 添加/修改窗体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00601")]
        [HttpPost]
        public ApiResult EditPermissionGroup(PermissionGroup model)
        {

            #region Old

            var ID = model.ID;
            if (model.ParentID == Guid.Empty)
            {
                model.ParentID = null;
            }
            var Parent = db.PermissionGroup.Where(a => a.ID == model.ParentID).FirstOrDefault();

            model.Path = (Parent?.Path ?? "/") + model.Name + "/";

            if (ID != Guid.Empty)
            {
                model.IdPath = (Parent?.IdPath ?? "/") + model.ID + "/";
                var oldpath = db.PermissionGroup.Where(a => a.ID == ID).Select(a => new { a.IdPath, a.Path }).FirstOrDefault();
                var listPath = db.PermissionGroup.Where(a => a.Path.StartsWith(oldpath.Path) && a.ID != ID && a.ParentID != model.ParentID).ToList();
                foreach (var item in listPath)
                {
                    if (item.Path != oldpath.Path)
                    {
                        item.Path = model.Path + item.Path.Substring(oldpath.Path.Length);
                    }
                }
                var listIdPath = db.PermissionGroup.Where(a => a.Path.StartsWith(oldpath.IdPath) && a.ID != ID && a.ParentID != model.ParentID).ToList();
                foreach (var item in listIdPath)
                {
                    if (item.IdPath != oldpath.IdPath)
                    {
                        item.IdPath = model.IdPath + item.IdPath.Substring(oldpath.IdPath.Length);
                    }
                }

                db.PermissionGroup.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                model.ID = Guid.NewGuid();
                model.IdPath = (Parent?.IdPath ?? "/") + model.ID + "/";
                model.IdPath.Replace(Guid.Empty.ToString(), model.ID.ToString());
                db.PermissionGroup.Add(model);
            }
            db.SaveChanges();

            #endregion



            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00601")]
        [HttpPost]
        public ApiResult DelPermissionGroup(Guid id)
        {
            var List = db.PermissionGroup.Where(a => a.ID == id).FirstOrDefault();
            db.PermissionGroup.Remove(List);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00601")]
        [HttpGet]
        public ApiResult<PermissionGroup> GetPermissionGroup(Guid? id)
        {
            var Model = db.PermissionGroup.Include("Application").Where(a => a.ID == id).FirstOrDefault();
            return new ApiResult<PermissionGroup> { Data = Model };
        }

        /// <summary>
        /// 获取窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00605")]
        [HttpGet]
        public ApiResult<List<PermissionGroup>> GetPermissionGroupByPID(Guid? id)
        {
            var list = db.PermissionGroup.Include("Application").Where(a => a.ParentID == id).OrderBy(a => a.Sort).ThenBy(a => a.Name).ToList();
            return new ApiResult<List<PermissionGroup>> { Data = list };
        }

        /// <summary>
        /// 根据名称获取窗体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [ActionName("CX00604")]
        [HttpGet]
        public ApiResult<PermissionGroup> GetPermissionGroupName(string name)
        {
            var Model = db.PermissionGroup.Where(a => a.Name == name).FirstOrDefault();

            return new ApiResult<PermissionGroup> { Data = Model };
        }

        /// <summary>
        /// 获得全部窗体
        /// </summary>
        /// <param name="ApplicationID"></param>
        /// <returns></returns>
        [ActionName("CX00603")]
        [HttpGet]
        public ApiResult<List<PermissionGroup>> GetPermissionGroupList(Guid? ApplicationID)
        {
            IQueryable<PermissionGroup> List = db.PermissionGroup.Include("Application").AsQueryable();
            if (ApplicationID != null)
            {
                List = List.Where(i => i.ApplicationID == ApplicationID);
            }

            return new ApiResult<List<PermissionGroup>> { Data = List.OrderBy(a => a.Sort).ThenBy(a => a.Name).ToList() };
        }


        /// <summary>
        /// 获得全部窗体
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="AppName">应用名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00602")]
        [HttpGet]
        public ApiResult<PagedList<sp_PermissionGroupQuery_Result>> GetPermissionGroupAll(string Name, string AppName, int PageIndex, int PageSize)
        {

            var Model = db.sp_PermissionGroupQuery(Name, AppName).ToList();
            return new ApiResult<PagedList<sp_PermissionGroupQuery_Result>> { Data = Model.AsQueryable().ToPagedList(PageIndex, PageSize) };
        }

        #endregion

        #region 窗体对应的按钮
        /// <summary>
        /// 添加/修改窗体对应的按钮
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00701")]
        [HttpPost]
        public ApiResult EditPermissionBtn(PermissionBtn model)
        {
            var ID = model.ID;
            if (model.ID == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                foreach (PermissionBtnInterface o in model.PermissionBtnInterface)
                {
                    o.PermissionButtonID = model.ID;
                }
                db.PermissionBtn.Add(model);
                db.SaveChanges();
            }
            else
            {
                db.PermissionBtn.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
                List<PermissionBtnInterface> list = model.PermissionBtnInterface.ToList();
                IQueryable<PermissionBtnInterface> listinterface = db.PermissionBtnInterface.Where(i => i.PermissionButtonID == model.ID);

                foreach (PermissionBtnInterface o in listinterface)
                {
                    db.PermissionBtnInterface.Attach(o);
                    db.PermissionBtnInterface.Remove(o);
                    //  db.SaveChanges();
                }
                foreach (PermissionBtnInterface obj in list)
                {
                    PermissionBtnInterface o = new PermissionBtnInterface();
                    o.ID = Guid.NewGuid();
                    o.PermissionButtonID = model.ID;
                    o.PermissionInterfaceID = obj.PermissionInterfaceID;
                    db.PermissionBtnInterface.Add(o);
                }
                db.SaveChanges();
            }

            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除窗体对应的按钮
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00701")]
        [HttpPost]
        public ApiResult DelPermissionBtn(Guid id)
        {
            db.PermissionBtn.Where(a => a.FormName == "1").Update(a => new PermissionBtn { FormName = "" });
            var PermissionBtn = new PermissionBtn() { ID = id };
            db.PermissionBtn.Attach(PermissionBtn);
            db.PermissionBtn.Remove(PermissionBtn);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取窗体对应的按钮
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00701")]
        [HttpGet]
        public ApiResult<PermissionBtn> GetPermissionBtn(Guid? id)
        {
            PermissionBtn Obj = db.PermissionBtn.Include("PermissionGroup.Application").Include("PermissionBtnInterface.PermissionInterface").FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<PermissionBtn> { Data = Obj };
        }

        /// <summary>
        /// 获得全部窗体对应的按钮
        /// </summary>
        /// <param name="ButtonName">按钮名</param>
        /// <param name="FormName">窗体名</param>
        /// <param name="DisplayName">显示名</param>
        /// <param name="ApplicationID">应用ID</param>
        /// <param name="PermissionGroupID">权限组ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00702")]
        [HttpGet]
        public ApiResult<PagedList<V_PermissionBtnQuery>> GetPermissionBtnAll(string ButtonName, string FormName, string DisplayName, Guid? ApplicationID, Guid? PermissionGroupID, int PageIndex, int PageSize)
        {
            IQueryable<V_PermissionBtnQuery> List = db.V_PermissionBtnQuery.Where(i => 1 == 1);

            if (ButtonName != null)
            {
                List = List.Where(i => i.ButtonName.Contains(ButtonName));
            }

            if (FormName != null)
            {
                List = List.Where(i => i.FormName.Contains(FormName));
            }

            if (DisplayName != null)
            {
                List = List.Where(i => i.DisplayName.Contains(DisplayName));
            }

            if (ApplicationID != null)
            {
                List = List.Where(i => i.ApplicationID == ApplicationID);
            }

            if (PermissionGroupID != null)
            {
                List = List.Where(i => i.PermissionGroupIdPath.Contains(PermissionGroupID + ""));
            }

            return new ApiResult<PagedList<V_PermissionBtnQuery>> { Data = List.OrderBy(i => i.PermissionGroupIdPath).ThenBy(i => i.FormName).ThenBy(i => i.ButtonName).ToPagedList(PageIndex, PageSize) };
        }

        #endregion

        #region 权限接口

        /// <summary>
        /// 添加/修改权限接口
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00703")]
        [HttpPost]
        public ApiResult EditPermissionInterface(PermissionInterface model)
        {
            var ID = model.ID;
            if (model.ID == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                db.PermissionInterface.Add(model);
            }
            else
            {
                db.PermissionInterface.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除权限接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00702")]
        [HttpPost]
        public ApiResult DelPermissionInterface(Guid id)
        {
            var PermissionInterface = new PermissionInterface() { ID = id };
            db.PermissionInterface.Attach(PermissionInterface);
            db.PermissionInterface.Remove(PermissionInterface);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取权限接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00703")]
        [HttpGet]
        public ApiResult<PermissionInterface> GetPermissionInterface(Guid? id)
        {
            PermissionInterface Obj = db.PermissionInterface.FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<PermissionInterface> { Data = Obj };
        }

        /// <summary>
        /// 获得全部权限接口
        /// </summary>
        /// <param name="InterfaceName">接口名</param>
        /// <param name="ApplicationID">应用ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00704")]
        [HttpGet]
        public ApiResult<PagedList<PermissionInterface>> GetPermissionInterfaceAll(string InterfaceName, Guid? ApplicationID, int PageIndex, int PageSize)
        {
            IQueryable<PermissionInterface> List = db.PermissionInterface.Include("Application").AsQueryable();

            if (InterfaceName != null)
            {
                List = List.Where(i => i.Name.Contains(InterfaceName));
            }

            if (ApplicationID != null)
            {
                List = List.Where(i => i.ApplicationID == ApplicationID);
            }

            return new ApiResult<PagedList<PermissionInterface>> { Data = List.OrderBy(i => i.Name).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获得全部权限接口
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="MethodName">方法名</param>
        /// <param name="ApplicationID">应用ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00705")]
        [HttpGet]
        public ApiResult<PagedList<V_PermissionInterfaceQuery>> GetPermissionInterfaceViewAll(string Name, string MethodName, Guid? ApplicationID, int PageIndex, int PageSize)
        {
            IQueryable<V_PermissionInterfaceQuery> List = db.V_PermissionInterfaceQuery.Where(i => 1 == 1);
            if (Name != null)
            {
                List = List.Where(i => i.Name.Contains(Name));
            }
            if (MethodName != null)
            {
                List = List.Where(i => i.MethodName.Contains(Name));
            }
            if (ApplicationID != null)
            {
                List = List.Where(i => i.ApplicationID == ApplicationID);
            }

            return new ApiResult<PagedList<V_PermissionInterfaceQuery>> { Data = List.OrderBy(i => i.Name).ToPagedList(PageIndex, PageSize) };

        }
        #endregion

        #region 用户角色关系

        /// <summary>
        /// 添加/修改用户角色关系
        /// </summary>
        /// <param name="lst">用户角色关系列表</param>
        /// <returns></returns>
        [ActionName("XG00801")]
        [HttpPost]
        public ApiResult EditUserToRole(List<UserToRole> lst)
        {
            Guid gUserID = lst[0].UserID;
            //删除用户对应的所有角色
            List<UserToRole> List = db.UserToRole.Where(i => i.UserID == gUserID).ToList();
            foreach (UserToRole item in List)
            {
                db.UserToRole.Remove(item);
            }
            Guid Gid = new Guid();
            if (!(lst.Count == 1 && lst[0].RoleID == Gid))
            {
                //批量新增
                foreach (UserToRole model in lst)
                {
                    model.ID = Guid.NewGuid();
                    db.UserToRole.Add(model);
                }
            }

            db.SaveChanges();
            return new ApiResult { Message = "操作成功！" };
        }

        /// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC00801")]
        [HttpPost]
        public ApiResult DelUserToRole(Guid id)
        {
            var UserToRole = new UserToRole() { ID = id };
            db.UserToRole.Attach(UserToRole);
            db.UserToRole.Remove(UserToRole);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00801")]
        [HttpGet]
        public ApiResult<UserToRole> GetUserToRole(Guid? id)
        {
            UserToRole Obj = db.UserToRole.FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<UserToRole> { Data = Obj };
        }

        /// <summary>
        /// 获得全部用户角色关系
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00802")]
        [HttpGet]
        public ApiResult<PagedList<UserToRole>> GetUserToRoleAll(string Name, int PageIndex, int PageSize)
        {
            string Str = "";
            if (Name != null)
            {
                Str = Name;
            }
            IQueryable<UserToRole> List = db.UserToRole.Where(i => i.User.Name.Contains(Str));
            return new ApiResult<PagedList<UserToRole>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }

        #endregion

        #region 角色权限
        /// <summary>
        /// 添加/修改用户角色关系
        /// </summary>
        /// <param Name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG00901")]
        [HttpPost]
        public ApiResult EditPermissionBtnRole(Guid RoleID, List<PermissionBtnRole> List)
        {
            //首先，删除所有旧的
            IQueryable<PermissionBtnRole> deleteliset = db.PermissionBtnRole.Where(i => i.RoleID == RoleID);
            foreach (PermissionBtnRole obj in deleteliset)
            {
                db.PermissionBtnRole.Attach(obj);
                db.PermissionBtnRole.Remove(obj);
            }
            db.SaveChanges();
            //然后，添加新的
            foreach (PermissionBtnRole model in List)
            {
                db.PermissionBtnRole.Add(model);
            }
            db.SaveChanges();
            return new ApiResult { Message = "设置成功！" };
        }

        /// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns></returns>
        [ActionName("SC00901")]
        [HttpPost]
        public ApiResult DelPermissionBtnRole(List<PermissionBtnRole> list)
        {
            if (list != null)
            {
                foreach (PermissionBtnRole obj in list)
                {
                    db.PermissionBtnRole.Attach(obj);
                    db.PermissionBtnRole.Remove(obj);
                    db.SaveChanges();
                }
            }
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX00901")]
        [HttpGet]
        public ApiResult<PermissionBtnRole> GetPermissionBtnRole(Guid? id)
        {
            PermissionBtnRole Obj = db.PermissionBtnRole.FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<PermissionBtnRole> { Data = Obj };
        }

        /// <summary>
        /// 获得全部用户角色关系
        /// </summary>
        /// <param name="RoleID">RoleID</param>

        /// <returns></returns>
        [ActionName("CX00902")]
        [HttpGet]
        public ApiResult<List<PermissionBtnRole>> GetPermissionBtnRoleAll(Guid RoleID)
        {
            List<PermissionBtnRole> List = db.PermissionBtnRole.Where(i => i.RoleID == RoleID).ToList();

            return new ApiResult<List<PermissionBtnRole>> { Data = List };
        }
        #endregion
    }
}
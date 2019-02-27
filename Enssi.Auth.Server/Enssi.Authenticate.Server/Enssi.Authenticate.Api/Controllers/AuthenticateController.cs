using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Enssi.Authenticate.Model;
using Enssi.Authenticate.Data;

namespace Enssi.Authenticate.Api.Controllers
{
    public class AuthenticateController : ApiController
    {
        EnssiAuthenticateEntities db => Api.DbEnssiAuthenticate;

        #region 应用表

        /// <summary>
        /// 添加/修改应用表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">应用表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00101")]
        [HttpPost]
        public ApiResult<Application> EditApplication(bool IsAdd, Application Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.Application.Add(Model);
            }
            else
            {
                db.Application.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<Application> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除应用表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC00101")]
        [HttpPost]
        public ApiResult DeleteApplication(Guid ID)
        {
            var model = new Application { ID = ID };
            db.Application.Attach(model);
            db.Application.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部应用表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">应用名称</param>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="Description"></param>
        /// <param name="OrderByApplication">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00101")]
        [HttpGet]
        public ApiResult<PagedList<Application>> GetApplicationAll(Guid? ID, string Name, string NameSpace, string Description, string OrderByApplication, string Includes, int PageIndex, int PageSize)
        {
            var result = GetApplicationAllSelector(ID, Name, NameSpace, Description, OrderByApplication, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<Application>> { Data = result.Data.CastPagedList<Application>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部应用表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">应用名称</param>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="Description"></param>
        /// <param name="OrderByApplication">排序条件</param>
        /// <param name="SelectorApplication">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00102")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetApplicationAllSelector(Guid? ID, string Name, string NameSpace, string Description, string OrderByApplication, string SelectorApplication, int PageIndex, int PageSize)
        {
			var list = db.Application.Includes(Utility.GetIncludes(SelectorApplication));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(!string.IsNullOrEmpty(NameSpace))
			{
				if(NameSpace.StartsWith("="))
				{
					var value = NameSpace.Substring(1);
					list = list.Where(a => a.NameSpace == value);
				}
				else
				{
					list = list.Where(a => a.NameSpace.Contains(NameSpace));
				}
			}
			if(!string.IsNullOrEmpty(Description))
			{
				if(Description.StartsWith("="))
				{
					var value = Description.Substring(1);
					list = list.Where(a => a.Description == value);
				}
				else
				{
					list = list.Where(a => a.Description.Contains(Description));
				}
			}

            var selector = Utility.DeserializeSelector<Application>(SelectorApplication);
            var orderByList = Utility.DeserializeOrderBy<Application>(OrderByApplication);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取应用表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX00103")]
        [HttpGet]
        public ApiResult<Application> GetApplication(Guid ID)
        {
            var model = db.Application.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<Application> { Data = model };
        }

        #endregion

        #region 审批日志

        /// <summary>
        /// 添加/修改审批日志
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">审批日志实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00201")]
        [HttpPost]
        public ApiResult<ApproveLog> EditApproveLog(bool IsAdd, ApproveLog Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.ApproveLog.Add(Model);
            }
            else
            {
                db.ApproveLog.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<ApproveLog> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除审批日志
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("SC00201")]
        [HttpPost]
        public ApiResult DeleteApproveLog(Guid ID)
        {
            var model = new ApproveLog { ID = ID };
            db.ApproveLog.Attach(model);
            db.ApproveLog.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部审批日志
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Keys">审批流通用Key</param>
        /// <param name="TemplateID">模板ID</param>
        /// <param name="TemplateDetailID">模板详情ID</param>
        /// <param name="Rank">流程环节序号</param>
        /// <param name="RankName">环节名称</param>
        /// <param name="Count">第几次</param>
        /// <param name="ApproveUserID">当前审批人编号</param>
        /// <param name="ApproveUserName">当前审批人名称</param>
        /// <param name="Result">审批结果</param>
        /// <param name="ResultState"></param>
        /// <param name="Reason">原因</param>
        /// <param name="ApproveTime">当前审批日期</param>
        /// <param name="HistoryData"></param>
        /// <param name="NextRank"></param>
        /// <param name="OrderByApproveLog">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00201")]
        [HttpGet]
        public ApiResult<PagedList<ApproveLog>> GetApproveLogAll(Guid? ID, string Keys, Guid? TemplateID, Guid? TemplateDetailID, int? Rank, string RankName, int? Count, Guid? ApproveUserID, string ApproveUserName, int? Result, string ResultState, string Reason, DateTime? ApproveTime, string HistoryData, int? NextRank, string OrderByApproveLog, string Includes, int PageIndex, int PageSize)
        {
            var result = GetApproveLogAllSelector(ID, Keys, TemplateID, TemplateDetailID, Rank, RankName, Count, ApproveUserID, ApproveUserName, Result, ResultState, Reason, ApproveTime, HistoryData, NextRank, OrderByApproveLog, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<ApproveLog>> { Data = result.Data.CastPagedList<ApproveLog>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部审批日志，自定义返回类型
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Keys">审批流通用Key</param>
        /// <param name="TemplateID">模板ID</param>
        /// <param name="TemplateDetailID">模板详情ID</param>
        /// <param name="Rank">流程环节序号</param>
        /// <param name="RankName">环节名称</param>
        /// <param name="Count">第几次</param>
        /// <param name="ApproveUserID">当前审批人编号</param>
        /// <param name="ApproveUserName">当前审批人名称</param>
        /// <param name="Result">审批结果</param>
        /// <param name="ResultState"></param>
        /// <param name="Reason">原因</param>
        /// <param name="ApproveTime">当前审批日期</param>
        /// <param name="HistoryData"></param>
        /// <param name="NextRank"></param>
        /// <param name="OrderByApproveLog">排序条件</param>
        /// <param name="SelectorApproveLog">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00202")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetApproveLogAllSelector(Guid? ID, string Keys, Guid? TemplateID, Guid? TemplateDetailID, int? Rank, string RankName, int? Count, Guid? ApproveUserID, string ApproveUserName, int? Result, string ResultState, string Reason, DateTime? ApproveTime, string HistoryData, int? NextRank, string OrderByApproveLog, string SelectorApproveLog, int PageIndex, int PageSize)
        {
			var list = db.ApproveLog.Includes(Utility.GetIncludes(SelectorApproveLog));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Keys))
			{
				if(Keys.StartsWith("="))
				{
					var value = Keys.Substring(1);
					list = list.Where(a => a.Keys == value);
				}
				else
				{
					list = list.Where(a => a.Keys.Contains(Keys));
				}
			}
			if(TemplateID != null)
			{
				list = list.Where(a => a.TemplateID == TemplateID);
			}
			if(TemplateDetailID != null)
			{
				list = list.Where(a => a.TemplateDetailID == TemplateDetailID);
			}
			if(Rank != null)
			{
				list = list.Where(a => a.Rank == Rank);
			}
			if(!string.IsNullOrEmpty(RankName))
			{
				if(RankName.StartsWith("="))
				{
					var value = RankName.Substring(1);
					list = list.Where(a => a.RankName == value);
				}
				else
				{
					list = list.Where(a => a.RankName.Contains(RankName));
				}
			}
			if(Count != null)
			{
				list = list.Where(a => a.Count == Count);
			}
			if(ApproveUserID != null)
			{
				list = list.Where(a => a.ApproveUserID == ApproveUserID);
			}
			if(!string.IsNullOrEmpty(ApproveUserName))
			{
				if(ApproveUserName.StartsWith("="))
				{
					var value = ApproveUserName.Substring(1);
					list = list.Where(a => a.ApproveUserName == value);
				}
				else
				{
					list = list.Where(a => a.ApproveUserName.Contains(ApproveUserName));
				}
			}
			if(Result != null)
			{
				list = list.Where(a => a.Result == Result);
			}
			if(!string.IsNullOrEmpty(ResultState))
			{
				if(ResultState.StartsWith("="))
				{
					var value = ResultState.Substring(1);
					list = list.Where(a => a.ResultState == value);
				}
				else
				{
					list = list.Where(a => a.ResultState.Contains(ResultState));
				}
			}
			if(!string.IsNullOrEmpty(Reason))
			{
				if(Reason.StartsWith("="))
				{
					var value = Reason.Substring(1);
					list = list.Where(a => a.Reason == value);
				}
				else
				{
					list = list.Where(a => a.Reason.Contains(Reason));
				}
			}
			if(ApproveTime != null)
			{
				list = list.Where(a => a.ApproveTime == ApproveTime);
			}
			if(!string.IsNullOrEmpty(HistoryData))
			{
				if(HistoryData.StartsWith("="))
				{
					var value = HistoryData.Substring(1);
					list = list.Where(a => a.HistoryData == value);
				}
				else
				{
					list = list.Where(a => a.HistoryData.Contains(HistoryData));
				}
			}
			if(NextRank != null)
			{
				list = list.Where(a => a.NextRank == NextRank);
			}

            var selector = Utility.DeserializeSelector<ApproveLog>(SelectorApproveLog);
            var orderByList = Utility.DeserializeOrderBy<ApproveLog>(OrderByApproveLog);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("CX00203")]
        [HttpGet]
        public ApiResult<ApproveLog> GetApproveLog(Guid ID)
        {
            var model = db.ApproveLog.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<ApproveLog> { Data = model };
        }

        #endregion

        #region 通用审批模板

        /// <summary>
        /// 添加/修改通用审批模板
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用审批模板实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00301")]
        [HttpPost]
        public ApiResult<ApproveTemplate> EditApproveTemplate(bool IsAdd, ApproveTemplate Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.ApproveTemplate.Add(Model);
            }
            else
            {
                db.ApproveTemplate.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<ApproveTemplate> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除通用审批模板
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("SC00301")]
        [HttpPost]
        public ApiResult DeleteApproveTemplate(Guid ID)
        {
            var model = new ApproveTemplate { ID = ID };
            db.ApproveTemplate.Attach(model);
            db.ApproveTemplate.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部通用审批模板
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="SystemName">应用名</param>
        /// <param name="ModuleName">模块名</param>
        /// <param name="Remark">审批流说明</param>
        /// <param name="PassState"></param>
        /// <param name="OrderByApproveTemplate">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00301")]
        [HttpGet]
        public ApiResult<PagedList<ApproveTemplate>> GetApproveTemplateAll(Guid? ID, string SystemName, string ModuleName, string Remark, string PassState, string OrderByApproveTemplate, string Includes, int PageIndex, int PageSize)
        {
            var result = GetApproveTemplateAllSelector(ID, SystemName, ModuleName, Remark, PassState, OrderByApproveTemplate, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<ApproveTemplate>> { Data = result.Data.CastPagedList<ApproveTemplate>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部通用审批模板，自定义返回类型
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="SystemName">应用名</param>
        /// <param name="ModuleName">模块名</param>
        /// <param name="Remark">审批流说明</param>
        /// <param name="PassState"></param>
        /// <param name="OrderByApproveTemplate">排序条件</param>
        /// <param name="SelectorApproveTemplate">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00302")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetApproveTemplateAllSelector(Guid? ID, string SystemName, string ModuleName, string Remark, string PassState, string OrderByApproveTemplate, string SelectorApproveTemplate, int PageIndex, int PageSize)
        {
			var list = db.ApproveTemplate.Includes(Utility.GetIncludes(SelectorApproveTemplate));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(SystemName))
			{
				if(SystemName.StartsWith("="))
				{
					var value = SystemName.Substring(1);
					list = list.Where(a => a.SystemName == value);
				}
				else
				{
					list = list.Where(a => a.SystemName.Contains(SystemName));
				}
			}
			if(!string.IsNullOrEmpty(ModuleName))
			{
				if(ModuleName.StartsWith("="))
				{
					var value = ModuleName.Substring(1);
					list = list.Where(a => a.ModuleName == value);
				}
				else
				{
					list = list.Where(a => a.ModuleName.Contains(ModuleName));
				}
			}
			if(!string.IsNullOrEmpty(Remark))
			{
				if(Remark.StartsWith("="))
				{
					var value = Remark.Substring(1);
					list = list.Where(a => a.Remark == value);
				}
				else
				{
					list = list.Where(a => a.Remark.Contains(Remark));
				}
			}
			if(!string.IsNullOrEmpty(PassState))
			{
				if(PassState.StartsWith("="))
				{
					var value = PassState.Substring(1);
					list = list.Where(a => a.PassState == value);
				}
				else
				{
					list = list.Where(a => a.PassState.Contains(PassState));
				}
			}

            var selector = Utility.DeserializeSelector<ApproveTemplate>(SelectorApproveTemplate);
            var orderByList = Utility.DeserializeOrderBy<ApproveTemplate>(OrderByApproveTemplate);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取通用审批模板
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("CX00303")]
        [HttpGet]
        public ApiResult<ApproveTemplate> GetApproveTemplate(Guid ID)
        {
            var model = db.ApproveTemplate.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<ApproveTemplate> { Data = model };
        }

        #endregion

        #region 通用审批模板明细

        /// <summary>
        /// 添加/修改通用审批模板明细
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用审批模板明细实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00401")]
        [HttpPost]
        public ApiResult<ApproveTemplateDetail> EditApproveTemplateDetail(bool IsAdd, ApproveTemplateDetail Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.ApproveTemplateDetail.Add(Model);
            }
            else
            {
                db.ApproveTemplateDetail.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<ApproveTemplateDetail> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除通用审批模板明细
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("SC00401")]
        [HttpPost]
        public ApiResult DeleteApproveTemplateDetail(Guid ID)
        {
            var model = new ApproveTemplateDetail { ID = ID };
            db.ApproveTemplateDetail.Attach(model);
            db.ApproveTemplateDetail.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部通用审批模板明细
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="TemplateID">模板ID</param>
        /// <param name="Rank">流程环节序号</param>
        /// <param name="RankName">环节名称</param>
        /// <param name="PassRank">通过后环节</param>
        /// <param name="PassState">通过后状态</param>
        /// <param name="RejectRank">拒绝后环节</param>
        /// <param name="RejectState">拒绝后状态</param>
        /// <param name="RoleID"></param>
        /// <param name="OrderByApproveTemplateDetail">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00401")]
        [HttpGet]
        public ApiResult<PagedList<ApproveTemplateDetail>> GetApproveTemplateDetailAll(Guid? ID, Guid? TemplateID, int? Rank, string RankName, int? PassRank, string PassState, int? RejectRank, string RejectState, Guid? RoleID, string OrderByApproveTemplateDetail, string Includes, int PageIndex, int PageSize)
        {
            var result = GetApproveTemplateDetailAllSelector(ID, TemplateID, Rank, RankName, PassRank, PassState, RejectRank, RejectState, RoleID, OrderByApproveTemplateDetail, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<ApproveTemplateDetail>> { Data = result.Data.CastPagedList<ApproveTemplateDetail>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部通用审批模板明细，自定义返回类型
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="TemplateID">模板ID</param>
        /// <param name="Rank">流程环节序号</param>
        /// <param name="RankName">环节名称</param>
        /// <param name="PassRank">通过后环节</param>
        /// <param name="PassState">通过后状态</param>
        /// <param name="RejectRank">拒绝后环节</param>
        /// <param name="RejectState">拒绝后状态</param>
        /// <param name="RoleID"></param>
        /// <param name="OrderByApproveTemplateDetail">排序条件</param>
        /// <param name="SelectorApproveTemplateDetail">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00402")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetApproveTemplateDetailAllSelector(Guid? ID, Guid? TemplateID, int? Rank, string RankName, int? PassRank, string PassState, int? RejectRank, string RejectState, Guid? RoleID, string OrderByApproveTemplateDetail, string SelectorApproveTemplateDetail, int PageIndex, int PageSize)
        {
			var list = db.ApproveTemplateDetail.Includes(Utility.GetIncludes(SelectorApproveTemplateDetail));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(TemplateID != null)
			{
				list = list.Where(a => a.TemplateID == TemplateID);
			}
			if(Rank != null)
			{
				list = list.Where(a => a.Rank == Rank);
			}
			if(!string.IsNullOrEmpty(RankName))
			{
				if(RankName.StartsWith("="))
				{
					var value = RankName.Substring(1);
					list = list.Where(a => a.RankName == value);
				}
				else
				{
					list = list.Where(a => a.RankName.Contains(RankName));
				}
			}
			if(PassRank != null)
			{
				list = list.Where(a => a.PassRank == PassRank);
			}
			if(!string.IsNullOrEmpty(PassState))
			{
				if(PassState.StartsWith("="))
				{
					var value = PassState.Substring(1);
					list = list.Where(a => a.PassState == value);
				}
				else
				{
					list = list.Where(a => a.PassState.Contains(PassState));
				}
			}
			if(RejectRank != null)
			{
				list = list.Where(a => a.RejectRank == RejectRank);
			}
			if(!string.IsNullOrEmpty(RejectState))
			{
				if(RejectState.StartsWith("="))
				{
					var value = RejectState.Substring(1);
					list = list.Where(a => a.RejectState == value);
				}
				else
				{
					list = list.Where(a => a.RejectState.Contains(RejectState));
				}
			}
			if(RoleID != null)
			{
				list = list.Where(a => a.RoleID == RoleID);
			}

            var selector = Utility.DeserializeSelector<ApproveTemplateDetail>(SelectorApproveTemplateDetail);
            var orderByList = Utility.DeserializeOrderBy<ApproveTemplateDetail>(OrderByApproveTemplateDetail);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取通用审批模板明细
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        [ActionName("CX00403")]
        [HttpGet]
        public ApiResult<ApproveTemplateDetail> GetApproveTemplateDetail(Guid ID)
        {
            var model = db.ApproveTemplateDetail.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<ApproveTemplateDetail> { Data = model };
        }

        #endregion

        #region CommonWords

        /// <summary>
        /// 添加/修改CommonWords
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">CommonWords实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00501")]
        [HttpPost]
        public ApiResult<CommonWords> EditCommonWords(bool IsAdd, CommonWords Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.CommonWords.Add(Model);
            }
            else
            {
                db.CommonWords.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<CommonWords> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除CommonWords
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC00501")]
        [HttpPost]
        public ApiResult DeleteCommonWords(Guid ID)
        {
            var model = new CommonWords { ID = ID };
            db.CommonWords.Attach(model);
            db.CommonWords.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部CommonWords
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CommonID">常用词缩写</param>
        /// <param name="CommonNo">常用词完整</param>
        /// <param name="CommonName">常用词名称</param>
        /// <param name="Remark">说明</param>
        /// <param name="OrderByCommonWords">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00501")]
        [HttpGet]
        public ApiResult<PagedList<CommonWords>> GetCommonWordsAll(Guid? ID, string CommonID, string CommonNo, string CommonName, string Remark, string OrderByCommonWords, string Includes, int PageIndex, int PageSize)
        {
            var result = GetCommonWordsAllSelector(ID, CommonID, CommonNo, CommonName, Remark, OrderByCommonWords, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<CommonWords>> { Data = result.Data.CastPagedList<CommonWords>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部CommonWords，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CommonID">常用词缩写</param>
        /// <param name="CommonNo">常用词完整</param>
        /// <param name="CommonName">常用词名称</param>
        /// <param name="Remark">说明</param>
        /// <param name="OrderByCommonWords">排序条件</param>
        /// <param name="SelectorCommonWords">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00502")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetCommonWordsAllSelector(Guid? ID, string CommonID, string CommonNo, string CommonName, string Remark, string OrderByCommonWords, string SelectorCommonWords, int PageIndex, int PageSize)
        {
			var list = db.CommonWords.Includes(Utility.GetIncludes(SelectorCommonWords));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(CommonID))
			{
				if(CommonID.StartsWith("="))
				{
					var value = CommonID.Substring(1);
					list = list.Where(a => a.CommonID == value);
				}
				else
				{
					list = list.Where(a => a.CommonID.Contains(CommonID));
				}
			}
			if(!string.IsNullOrEmpty(CommonNo))
			{
				if(CommonNo.StartsWith("="))
				{
					var value = CommonNo.Substring(1);
					list = list.Where(a => a.CommonNo == value);
				}
				else
				{
					list = list.Where(a => a.CommonNo.Contains(CommonNo));
				}
			}
			if(!string.IsNullOrEmpty(CommonName))
			{
				if(CommonName.StartsWith("="))
				{
					var value = CommonName.Substring(1);
					list = list.Where(a => a.CommonName == value);
				}
				else
				{
					list = list.Where(a => a.CommonName.Contains(CommonName));
				}
			}
			if(!string.IsNullOrEmpty(Remark))
			{
				if(Remark.StartsWith("="))
				{
					var value = Remark.Substring(1);
					list = list.Where(a => a.Remark == value);
				}
				else
				{
					list = list.Where(a => a.Remark.Contains(Remark));
				}
			}

            var selector = Utility.DeserializeSelector<CommonWords>(SelectorCommonWords);
            var orderByList = Utility.DeserializeOrderBy<CommonWords>(OrderByCommonWords);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取CommonWords
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX00503")]
        [HttpGet]
        public ApiResult<CommonWords> GetCommonWords(Guid ID)
        {
            var model = db.CommonWords.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<CommonWords> { Data = model };
        }

        #endregion

        #region InterfaceDetail

        /// <summary>
        /// 添加/修改InterfaceDetail
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceDetail实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00601")]
        [HttpPost]
        public ApiResult<InterfaceDetail> EditInterfaceDetail(bool IsAdd, InterfaceDetail Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.InterfaceDetail.Add(Model);
            }
            else
            {
                db.InterfaceDetail.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<InterfaceDetail> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除InterfaceDetail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC00601")]
        [HttpPost]
        public ApiResult DeleteInterfaceDetail(Guid ID)
        {
            var model = new InterfaceDetail { ID = ID };
            db.InterfaceDetail.Attach(model);
            db.InterfaceDetail.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部InterfaceDetail
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="InterfaceID">接口编号(传自主表)</param>
        /// <param name="ParameterNo">参数编码</param>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="ParameterType">类型</param>
        /// <param name="ParameterLen">最大长度</param>
        /// <param name="IsNull">是否必填</param>
        /// <param name="OrderByInterfaceDetail">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00601")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceDetail>> GetInterfaceDetailAll(Guid? ID, Guid? InterfaceID, string ParameterNo, string ParameterName, string ParameterType, int? ParameterLen, int? IsNull, string OrderByInterfaceDetail, string Includes, int PageIndex, int PageSize)
        {
            var result = GetInterfaceDetailAllSelector(ID, InterfaceID, ParameterNo, ParameterName, ParameterType, ParameterLen, IsNull, OrderByInterfaceDetail, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<InterfaceDetail>> { Data = result.Data.CastPagedList<InterfaceDetail>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部InterfaceDetail，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="InterfaceID">接口编号(传自主表)</param>
        /// <param name="ParameterNo">参数编码</param>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="ParameterType">类型</param>
        /// <param name="ParameterLen">最大长度</param>
        /// <param name="IsNull">是否必填</param>
        /// <param name="OrderByInterfaceDetail">排序条件</param>
        /// <param name="SelectorInterfaceDetail">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00602")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetInterfaceDetailAllSelector(Guid? ID, Guid? InterfaceID, string ParameterNo, string ParameterName, string ParameterType, int? ParameterLen, int? IsNull, string OrderByInterfaceDetail, string SelectorInterfaceDetail, int PageIndex, int PageSize)
        {
			var list = db.InterfaceDetail.Includes(Utility.GetIncludes(SelectorInterfaceDetail));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(InterfaceID != null)
			{
				list = list.Where(a => a.InterfaceID == InterfaceID);
			}
			if(!string.IsNullOrEmpty(ParameterNo))
			{
				if(ParameterNo.StartsWith("="))
				{
					var value = ParameterNo.Substring(1);
					list = list.Where(a => a.ParameterNo == value);
				}
				else
				{
					list = list.Where(a => a.ParameterNo.Contains(ParameterNo));
				}
			}
			if(!string.IsNullOrEmpty(ParameterName))
			{
				if(ParameterName.StartsWith("="))
				{
					var value = ParameterName.Substring(1);
					list = list.Where(a => a.ParameterName == value);
				}
				else
				{
					list = list.Where(a => a.ParameterName.Contains(ParameterName));
				}
			}
			if(!string.IsNullOrEmpty(ParameterType))
			{
				if(ParameterType.StartsWith("="))
				{
					var value = ParameterType.Substring(1);
					list = list.Where(a => a.ParameterType == value);
				}
				else
				{
					list = list.Where(a => a.ParameterType.Contains(ParameterType));
				}
			}
			if(ParameterLen != null)
			{
				list = list.Where(a => a.ParameterLen == ParameterLen);
			}
			if(IsNull != null)
			{
				list = list.Where(a => a.IsNull == IsNull);
			}

            var selector = Utility.DeserializeSelector<InterfaceDetail>(SelectorInterfaceDetail);
            var orderByList = Utility.DeserializeOrderBy<InterfaceDetail>(OrderByInterfaceDetail);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取InterfaceDetail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX00603")]
        [HttpGet]
        public ApiResult<InterfaceDetail> GetInterfaceDetail(Guid ID)
        {
            var model = db.InterfaceDetail.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<InterfaceDetail> { Data = model };
        }

        #endregion

        #region InterfaceDetailHist

        /// <summary>
        /// 添加/修改InterfaceDetailHist
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceDetailHist实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00701")]
        [HttpPost]
        public ApiResult<InterfaceDetailHist> EditInterfaceDetailHist(bool IsAdd, InterfaceDetailHist Model)
        {
            if (IsAdd)
            {
				if(Model.PK == Guid.Empty)
				{
					Model.PK = Guid.NewGuid();
				}
                db.InterfaceDetailHist.Add(Model);
            }
            else
            {
                db.InterfaceDetailHist.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<InterfaceDetailHist> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除InterfaceDetailHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        [ActionName("SC00701")]
        [HttpPost]
        public ApiResult DeleteInterfaceDetailHist(Guid PK)
        {
            var model = new InterfaceDetailHist { PK = PK };
            db.InterfaceDetailHist.Attach(model);
            db.InterfaceDetailHist.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部InterfaceDetailHist
        /// </summary>
        /// <param name="PK"></param>
        /// <param name="ID"></param>
        /// <param name="InterfaceID"></param>
        /// <param name="ParameterNo"></param>
        /// <param name="ParameterName"></param>
        /// <param name="ParameterType"></param>
        /// <param name="ParameterLen"></param>
        /// <param name="IsNull"></param>
        /// <param name="Flag"></param>
        /// <param name="OrderByInterfaceDetailHist">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00701")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceDetailHist>> GetInterfaceDetailHistAll(Guid? PK, Guid? ID, Guid? InterfaceID, string ParameterNo, string ParameterName, string ParameterType, int? ParameterLen, int? IsNull, string Flag, string OrderByInterfaceDetailHist, string Includes, int PageIndex, int PageSize)
        {
            var result = GetInterfaceDetailHistAllSelector(PK, ID, InterfaceID, ParameterNo, ParameterName, ParameterType, ParameterLen, IsNull, Flag, OrderByInterfaceDetailHist, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<InterfaceDetailHist>> { Data = result.Data.CastPagedList<InterfaceDetailHist>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部InterfaceDetailHist，自定义返回类型
        /// </summary>
        /// <param name="PK"></param>
        /// <param name="ID"></param>
        /// <param name="InterfaceID"></param>
        /// <param name="ParameterNo"></param>
        /// <param name="ParameterName"></param>
        /// <param name="ParameterType"></param>
        /// <param name="ParameterLen"></param>
        /// <param name="IsNull"></param>
        /// <param name="Flag"></param>
        /// <param name="OrderByInterfaceDetailHist">排序条件</param>
        /// <param name="SelectorInterfaceDetailHist">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00702")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetInterfaceDetailHistAllSelector(Guid? PK, Guid? ID, Guid? InterfaceID, string ParameterNo, string ParameterName, string ParameterType, int? ParameterLen, int? IsNull, string Flag, string OrderByInterfaceDetailHist, string SelectorInterfaceDetailHist, int PageIndex, int PageSize)
        {
			var list = db.InterfaceDetailHist.Includes(Utility.GetIncludes(SelectorInterfaceDetailHist));

			if(PK != null)
			{
				list = list.Where(a => a.PK == PK);
			}
			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(InterfaceID != null)
			{
				list = list.Where(a => a.InterfaceID == InterfaceID);
			}
			if(!string.IsNullOrEmpty(ParameterNo))
			{
				if(ParameterNo.StartsWith("="))
				{
					var value = ParameterNo.Substring(1);
					list = list.Where(a => a.ParameterNo == value);
				}
				else
				{
					list = list.Where(a => a.ParameterNo.Contains(ParameterNo));
				}
			}
			if(!string.IsNullOrEmpty(ParameterName))
			{
				if(ParameterName.StartsWith("="))
				{
					var value = ParameterName.Substring(1);
					list = list.Where(a => a.ParameterName == value);
				}
				else
				{
					list = list.Where(a => a.ParameterName.Contains(ParameterName));
				}
			}
			if(!string.IsNullOrEmpty(ParameterType))
			{
				if(ParameterType.StartsWith("="))
				{
					var value = ParameterType.Substring(1);
					list = list.Where(a => a.ParameterType == value);
				}
				else
				{
					list = list.Where(a => a.ParameterType.Contains(ParameterType));
				}
			}
			if(ParameterLen != null)
			{
				list = list.Where(a => a.ParameterLen == ParameterLen);
			}
			if(IsNull != null)
			{
				list = list.Where(a => a.IsNull == IsNull);
			}
			if(!string.IsNullOrEmpty(Flag))
			{
				if(Flag.StartsWith("="))
				{
					var value = Flag.Substring(1);
					list = list.Where(a => a.Flag == value);
				}
				else
				{
					list = list.Where(a => a.Flag.Contains(Flag));
				}
			}

            var selector = Utility.DeserializeSelector<InterfaceDetailHist>(SelectorInterfaceDetailHist);
            var orderByList = Utility.DeserializeOrderBy<InterfaceDetailHist>(OrderByInterfaceDetailHist);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.PK });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取InterfaceDetailHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        [ActionName("CX00703")]
        [HttpGet]
        public ApiResult<InterfaceDetailHist> GetInterfaceDetailHist(Guid PK)
        {
            var model = db.InterfaceDetailHist.Where(a => a.PK == PK).FirstOrDefault();
            return new ApiResult<InterfaceDetailHist> { Data = model };
        }

        #endregion

        #region InterfaceMain

        /// <summary>
        /// 添加/修改InterfaceMain
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceMain实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00801")]
        [HttpPost]
        public ApiResult<InterfaceMain> EditInterfaceMain(bool IsAdd, InterfaceMain Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.InterfaceMain.Add(Model);
            }
            else
            {
                db.InterfaceMain.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<InterfaceMain> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除InterfaceMain
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC00801")]
        [HttpPost]
        public ApiResult DeleteInterfaceMain(Guid ID)
        {
            var model = new InterfaceMain { ID = ID };
            db.InterfaceMain.Attach(model);
            db.InterfaceMain.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部InterfaceMain
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="InterfaceNo">接口编码</param>
        /// <param name="BelongSystem">所属系统</param>
        /// <param name="BelongModule">所属模块</param>
        /// <param name="Operation">操作类型</param>
        /// <param name="InExample">输入参数</param>
        /// <param name="OutExample">输出参数</param>
        /// <param name="Parameter">举例</param>
        /// <param name="Remark">说明</param>
        /// <param name="IsDeleted">是否删除</param>
        /// <param name="CreateUserID">创建人</param>
        /// <param name="CreateDate">创建日期</param>
        /// <param name="OrderByInterfaceMain">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00801")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceMain>> GetInterfaceMainAll(Guid? ID, string InterfaceNo, string BelongSystem, string BelongModule, string Operation, string InExample, string OutExample, string Parameter, string Remark, int? IsDeleted, Guid? CreateUserID, DateTime? CreateDate, string OrderByInterfaceMain, string Includes, int PageIndex, int PageSize)
        {
            var result = GetInterfaceMainAllSelector(ID, InterfaceNo, BelongSystem, BelongModule, Operation, InExample, OutExample, Parameter, Remark, IsDeleted, CreateUserID, CreateDate, OrderByInterfaceMain, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<InterfaceMain>> { Data = result.Data.CastPagedList<InterfaceMain>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部InterfaceMain，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="InterfaceNo">接口编码</param>
        /// <param name="BelongSystem">所属系统</param>
        /// <param name="BelongModule">所属模块</param>
        /// <param name="Operation">操作类型</param>
        /// <param name="InExample">输入参数</param>
        /// <param name="OutExample">输出参数</param>
        /// <param name="Parameter">举例</param>
        /// <param name="Remark">说明</param>
        /// <param name="IsDeleted">是否删除</param>
        /// <param name="CreateUserID">创建人</param>
        /// <param name="CreateDate">创建日期</param>
        /// <param name="OrderByInterfaceMain">排序条件</param>
        /// <param name="SelectorInterfaceMain">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00802")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetInterfaceMainAllSelector(Guid? ID, string InterfaceNo, string BelongSystem, string BelongModule, string Operation, string InExample, string OutExample, string Parameter, string Remark, int? IsDeleted, Guid? CreateUserID, DateTime? CreateDate, string OrderByInterfaceMain, string SelectorInterfaceMain, int PageIndex, int PageSize)
        {
			var list = db.InterfaceMain.Includes(Utility.GetIncludes(SelectorInterfaceMain));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(InterfaceNo))
			{
				if(InterfaceNo.StartsWith("="))
				{
					var value = InterfaceNo.Substring(1);
					list = list.Where(a => a.InterfaceNo == value);
				}
				else
				{
					list = list.Where(a => a.InterfaceNo.Contains(InterfaceNo));
				}
			}
			if(!string.IsNullOrEmpty(BelongSystem))
			{
				if(BelongSystem.StartsWith("="))
				{
					var value = BelongSystem.Substring(1);
					list = list.Where(a => a.BelongSystem == value);
				}
				else
				{
					list = list.Where(a => a.BelongSystem.Contains(BelongSystem));
				}
			}
			if(!string.IsNullOrEmpty(BelongModule))
			{
				if(BelongModule.StartsWith("="))
				{
					var value = BelongModule.Substring(1);
					list = list.Where(a => a.BelongModule == value);
				}
				else
				{
					list = list.Where(a => a.BelongModule.Contains(BelongModule));
				}
			}
			if(!string.IsNullOrEmpty(Operation))
			{
				if(Operation.StartsWith("="))
				{
					var value = Operation.Substring(1);
					list = list.Where(a => a.Operation == value);
				}
				else
				{
					list = list.Where(a => a.Operation.Contains(Operation));
				}
			}
			if(!string.IsNullOrEmpty(InExample))
			{
				if(InExample.StartsWith("="))
				{
					var value = InExample.Substring(1);
					list = list.Where(a => a.InExample == value);
				}
				else
				{
					list = list.Where(a => a.InExample.Contains(InExample));
				}
			}
			if(!string.IsNullOrEmpty(OutExample))
			{
				if(OutExample.StartsWith("="))
				{
					var value = OutExample.Substring(1);
					list = list.Where(a => a.OutExample == value);
				}
				else
				{
					list = list.Where(a => a.OutExample.Contains(OutExample));
				}
			}
			if(!string.IsNullOrEmpty(Parameter))
			{
				if(Parameter.StartsWith("="))
				{
					var value = Parameter.Substring(1);
					list = list.Where(a => a.Parameter == value);
				}
				else
				{
					list = list.Where(a => a.Parameter.Contains(Parameter));
				}
			}
			if(!string.IsNullOrEmpty(Remark))
			{
				if(Remark.StartsWith("="))
				{
					var value = Remark.Substring(1);
					list = list.Where(a => a.Remark == value);
				}
				else
				{
					list = list.Where(a => a.Remark.Contains(Remark));
				}
			}
			if(IsDeleted != null)
			{
				list = list.Where(a => a.IsDeleted == IsDeleted);
			}
			if(CreateUserID != null)
			{
				list = list.Where(a => a.CreateUserID == CreateUserID);
			}
			if(CreateDate != null)
			{
				list = list.Where(a => a.CreateDate == CreateDate);
			}

            var selector = Utility.DeserializeSelector<InterfaceMain>(SelectorInterfaceMain);
            var orderByList = Utility.DeserializeOrderBy<InterfaceMain>(OrderByInterfaceMain);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取InterfaceMain
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX00803")]
        [HttpGet]
        public ApiResult<InterfaceMain> GetInterfaceMain(Guid ID)
        {
            var model = db.InterfaceMain.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<InterfaceMain> { Data = model };
        }

        #endregion

        #region InterfaceMainHist

        /// <summary>
        /// 添加/修改InterfaceMainHist
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceMainHist实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG00901")]
        [HttpPost]
        public ApiResult<InterfaceMainHist> EditInterfaceMainHist(bool IsAdd, InterfaceMainHist Model)
        {
            if (IsAdd)
            {
				if(Model.PK == Guid.Empty)
				{
					Model.PK = Guid.NewGuid();
				}
                db.InterfaceMainHist.Add(Model);
            }
            else
            {
                db.InterfaceMainHist.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<InterfaceMainHist> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除InterfaceMainHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        [ActionName("SC00901")]
        [HttpPost]
        public ApiResult DeleteInterfaceMainHist(Guid PK)
        {
            var model = new InterfaceMainHist { PK = PK };
            db.InterfaceMainHist.Attach(model);
            db.InterfaceMainHist.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部InterfaceMainHist
        /// </summary>
        /// <param name="PK"></param>
        /// <param name="ID"></param>
        /// <param name="InterfaceNo"></param>
        /// <param name="BelongSystem"></param>
        /// <param name="BelongModule"></param>
        /// <param name="Operation"></param>
        /// <param name="InExample"></param>
        /// <param name="OutExample"></param>
        /// <param name="Parameter"></param>
        /// <param name="Remark"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="CreateUserID"></param>
        /// <param name="CreateDate"></param>
        /// <param name="Flag"></param>
        /// <param name="OrderByInterfaceMainHist">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00901")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceMainHist>> GetInterfaceMainHistAll(Guid? PK, Guid? ID, string InterfaceNo, string BelongSystem, string BelongModule, string Operation, string InExample, string OutExample, string Parameter, string Remark, int? IsDeleted, Guid? CreateUserID, DateTime? CreateDate, string Flag, string OrderByInterfaceMainHist, string Includes, int PageIndex, int PageSize)
        {
            var result = GetInterfaceMainHistAllSelector(PK, ID, InterfaceNo, BelongSystem, BelongModule, Operation, InExample, OutExample, Parameter, Remark, IsDeleted, CreateUserID, CreateDate, Flag, OrderByInterfaceMainHist, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<InterfaceMainHist>> { Data = result.Data.CastPagedList<InterfaceMainHist>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部InterfaceMainHist，自定义返回类型
        /// </summary>
        /// <param name="PK"></param>
        /// <param name="ID"></param>
        /// <param name="InterfaceNo"></param>
        /// <param name="BelongSystem"></param>
        /// <param name="BelongModule"></param>
        /// <param name="Operation"></param>
        /// <param name="InExample"></param>
        /// <param name="OutExample"></param>
        /// <param name="Parameter"></param>
        /// <param name="Remark"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="CreateUserID"></param>
        /// <param name="CreateDate"></param>
        /// <param name="Flag"></param>
        /// <param name="OrderByInterfaceMainHist">排序条件</param>
        /// <param name="SelectorInterfaceMainHist">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX00902")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetInterfaceMainHistAllSelector(Guid? PK, Guid? ID, string InterfaceNo, string BelongSystem, string BelongModule, string Operation, string InExample, string OutExample, string Parameter, string Remark, int? IsDeleted, Guid? CreateUserID, DateTime? CreateDate, string Flag, string OrderByInterfaceMainHist, string SelectorInterfaceMainHist, int PageIndex, int PageSize)
        {
			var list = db.InterfaceMainHist.Includes(Utility.GetIncludes(SelectorInterfaceMainHist));

			if(PK != null)
			{
				list = list.Where(a => a.PK == PK);
			}
			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(InterfaceNo))
			{
				if(InterfaceNo.StartsWith("="))
				{
					var value = InterfaceNo.Substring(1);
					list = list.Where(a => a.InterfaceNo == value);
				}
				else
				{
					list = list.Where(a => a.InterfaceNo.Contains(InterfaceNo));
				}
			}
			if(!string.IsNullOrEmpty(BelongSystem))
			{
				if(BelongSystem.StartsWith("="))
				{
					var value = BelongSystem.Substring(1);
					list = list.Where(a => a.BelongSystem == value);
				}
				else
				{
					list = list.Where(a => a.BelongSystem.Contains(BelongSystem));
				}
			}
			if(!string.IsNullOrEmpty(BelongModule))
			{
				if(BelongModule.StartsWith("="))
				{
					var value = BelongModule.Substring(1);
					list = list.Where(a => a.BelongModule == value);
				}
				else
				{
					list = list.Where(a => a.BelongModule.Contains(BelongModule));
				}
			}
			if(!string.IsNullOrEmpty(Operation))
			{
				if(Operation.StartsWith("="))
				{
					var value = Operation.Substring(1);
					list = list.Where(a => a.Operation == value);
				}
				else
				{
					list = list.Where(a => a.Operation.Contains(Operation));
				}
			}
			if(!string.IsNullOrEmpty(InExample))
			{
				if(InExample.StartsWith("="))
				{
					var value = InExample.Substring(1);
					list = list.Where(a => a.InExample == value);
				}
				else
				{
					list = list.Where(a => a.InExample.Contains(InExample));
				}
			}
			if(!string.IsNullOrEmpty(OutExample))
			{
				if(OutExample.StartsWith("="))
				{
					var value = OutExample.Substring(1);
					list = list.Where(a => a.OutExample == value);
				}
				else
				{
					list = list.Where(a => a.OutExample.Contains(OutExample));
				}
			}
			if(!string.IsNullOrEmpty(Parameter))
			{
				if(Parameter.StartsWith("="))
				{
					var value = Parameter.Substring(1);
					list = list.Where(a => a.Parameter == value);
				}
				else
				{
					list = list.Where(a => a.Parameter.Contains(Parameter));
				}
			}
			if(!string.IsNullOrEmpty(Remark))
			{
				if(Remark.StartsWith("="))
				{
					var value = Remark.Substring(1);
					list = list.Where(a => a.Remark == value);
				}
				else
				{
					list = list.Where(a => a.Remark.Contains(Remark));
				}
			}
			if(IsDeleted != null)
			{
				list = list.Where(a => a.IsDeleted == IsDeleted);
			}
			if(CreateUserID != null)
			{
				list = list.Where(a => a.CreateUserID == CreateUserID);
			}
			if(CreateDate != null)
			{
				list = list.Where(a => a.CreateDate == CreateDate);
			}
			if(!string.IsNullOrEmpty(Flag))
			{
				if(Flag.StartsWith("="))
				{
					var value = Flag.Substring(1);
					list = list.Where(a => a.Flag == value);
				}
				else
				{
					list = list.Where(a => a.Flag.Contains(Flag));
				}
			}

            var selector = Utility.DeserializeSelector<InterfaceMainHist>(SelectorInterfaceMainHist);
            var orderByList = Utility.DeserializeOrderBy<InterfaceMainHist>(OrderByInterfaceMainHist);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.PK });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取InterfaceMainHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        [ActionName("CX00903")]
        [HttpGet]
        public ApiResult<InterfaceMainHist> GetInterfaceMainHist(Guid PK)
        {
            var model = db.InterfaceMainHist.Where(a => a.PK == PK).FirstOrDefault();
            return new ApiResult<InterfaceMainHist> { Data = model };
        }

        #endregion

        #region 组织架构表

        /// <summary>
        /// 添加/修改组织架构表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">组织架构表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01001")]
        [HttpPost]
        public ApiResult<Organization> EditOrganization(bool IsAdd, Organization Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.Organization.Add(Model);
            }
            else
            {
                db.Organization.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<Organization> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除组织架构表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01001")]
        [HttpPost]
        public ApiResult DeleteOrganization(Guid ID)
        {
            var model = new Organization { ID = ID };
            db.Organization.Attach(model);
            db.Organization.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部组织架构表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">组织架构名称</param>
        /// <param name="Type">组织架构类型</param>
        /// <param name="ParentID">父级Id</param>
        /// <param name="Sort">排序</param>
        /// <param name="Path">名称路径</param>
        /// <param name="IdPath">ID路径</param>
        /// <param name="GeneralID">通用组织ID</param>
        /// <param name="OrderByOrganization">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01001")]
        [HttpGet]
        public ApiResult<PagedList<Organization>> GetOrganizationAll(Guid? ID, string Name, string Type, Guid? ParentID, int? Sort, string Path, string IdPath, Guid? GeneralID, string OrderByOrganization, string Includes, int PageIndex, int PageSize)
        {
            var result = GetOrganizationAllSelector(ID, Name, Type, ParentID, Sort, Path, IdPath, GeneralID, OrderByOrganization, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<Organization>> { Data = result.Data.CastPagedList<Organization>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部组织架构表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">组织架构名称</param>
        /// <param name="Type">组织架构类型</param>
        /// <param name="ParentID">父级Id</param>
        /// <param name="Sort">排序</param>
        /// <param name="Path">名称路径</param>
        /// <param name="IdPath">ID路径</param>
        /// <param name="GeneralID">通用组织ID</param>
        /// <param name="OrderByOrganization">排序条件</param>
        /// <param name="SelectorOrganization">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01002")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetOrganizationAllSelector(Guid? ID, string Name, string Type, Guid? ParentID, int? Sort, string Path, string IdPath, Guid? GeneralID, string OrderByOrganization, string SelectorOrganization, int PageIndex, int PageSize)
        {
			var list = db.Organization.Includes(Utility.GetIncludes(SelectorOrganization));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(!string.IsNullOrEmpty(Type))
			{
				if(Type.StartsWith("="))
				{
					var value = Type.Substring(1);
					list = list.Where(a => a.Type == value);
				}
				else
				{
					list = list.Where(a => a.Type.Contains(Type));
				}
			}
			if(ParentID != null)
			{
				list = list.Where(a => a.ParentID == ParentID);
			}
			if(Sort != null)
			{
				list = list.Where(a => a.Sort == Sort);
			}
			if(!string.IsNullOrEmpty(Path))
			{
				if(Path.StartsWith("="))
				{
					var value = Path.Substring(1);
					list = list.Where(a => a.Path == value);
				}
				else
				{
					list = list.Where(a => a.Path.Contains(Path));
				}
			}
			if(!string.IsNullOrEmpty(IdPath))
			{
				if(IdPath.StartsWith("="))
				{
					var value = IdPath.Substring(1);
					list = list.Where(a => a.IdPath == value);
				}
				else
				{
					list = list.Where(a => a.IdPath.Contains(IdPath));
				}
			}
			if(GeneralID != null)
			{
				list = list.Where(a => a.GeneralID == GeneralID);
			}

            var selector = Utility.DeserializeSelector<Organization>(SelectorOrganization);
            var orderByList = Utility.DeserializeOrderBy<Organization>(OrderByOrganization);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取组织架构表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01003")]
        [HttpGet]
        public ApiResult<Organization> GetOrganization(Guid ID)
        {
            var model = db.Organization.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<Organization> { Data = model };
        }

        #endregion

        #region 通用组织

        /// <summary>
        /// 添加/修改通用组织
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用组织实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01101")]
        [HttpPost]
        public ApiResult<OrganizationGeneral> EditOrganizationGeneral(bool IsAdd, OrganizationGeneral Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.OrganizationGeneral.Add(Model);
            }
            else
            {
                db.OrganizationGeneral.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<OrganizationGeneral> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除通用组织
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
        [ActionName("SC01101")]
        [HttpPost]
        public ApiResult DeleteOrganizationGeneral(Guid ID)
        {
            var model = new OrganizationGeneral { ID = ID };
            db.OrganizationGeneral.Attach(model);
            db.OrganizationGeneral.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部通用组织
        /// </summary>
        /// <param name="ID">编号</param>
        /// <param name="Name">名称</param>
        /// <param name="Type">类型</param>
        /// <param name="Description">说明</param>
        /// <param name="Group">分组</param>
        /// <param name="OrderByOrganizationGeneral">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01101")]
        [HttpGet]
        public ApiResult<PagedList<OrganizationGeneral>> GetOrganizationGeneralAll(Guid? ID, string Name, string Type, string Description, string Group, string OrderByOrganizationGeneral, string Includes, int PageIndex, int PageSize)
        {
            var result = GetOrganizationGeneralAllSelector(ID, Name, Type, Description, Group, OrderByOrganizationGeneral, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<OrganizationGeneral>> { Data = result.Data.CastPagedList<OrganizationGeneral>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部通用组织，自定义返回类型
        /// </summary>
        /// <param name="ID">编号</param>
        /// <param name="Name">名称</param>
        /// <param name="Type">类型</param>
        /// <param name="Description">说明</param>
        /// <param name="Group">分组</param>
        /// <param name="OrderByOrganizationGeneral">排序条件</param>
        /// <param name="SelectorOrganizationGeneral">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01102")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetOrganizationGeneralAllSelector(Guid? ID, string Name, string Type, string Description, string Group, string OrderByOrganizationGeneral, string SelectorOrganizationGeneral, int PageIndex, int PageSize)
        {
			var list = db.OrganizationGeneral.Includes(Utility.GetIncludes(SelectorOrganizationGeneral));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(!string.IsNullOrEmpty(Type))
			{
				if(Type.StartsWith("="))
				{
					var value = Type.Substring(1);
					list = list.Where(a => a.Type == value);
				}
				else
				{
					list = list.Where(a => a.Type.Contains(Type));
				}
			}
			if(!string.IsNullOrEmpty(Description))
			{
				if(Description.StartsWith("="))
				{
					var value = Description.Substring(1);
					list = list.Where(a => a.Description == value);
				}
				else
				{
					list = list.Where(a => a.Description.Contains(Description));
				}
			}
			if(!string.IsNullOrEmpty(Group))
			{
				if(Group.StartsWith("="))
				{
					var value = Group.Substring(1);
					list = list.Where(a => a.Group == value);
				}
				else
				{
					list = list.Where(a => a.Group.Contains(Group));
				}
			}

            var selector = Utility.DeserializeSelector<OrganizationGeneral>(SelectorOrganizationGeneral);
            var orderByList = Utility.DeserializeOrderBy<OrganizationGeneral>(OrderByOrganizationGeneral);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取通用组织
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
        [ActionName("CX01103")]
        [HttpGet]
        public ApiResult<OrganizationGeneral> GetOrganizationGeneral(Guid ID)
        {
            var model = db.OrganizationGeneral.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<OrganizationGeneral> { Data = model };
        }

        #endregion

        #region 权限按钮表

        /// <summary>
        /// 添加/修改权限按钮表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01201")]
        [HttpPost]
        public ApiResult<PermissionBtn> EditPermissionBtn(bool IsAdd, PermissionBtn Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionBtn.Add(Model);
            }
            else
            {
                db.PermissionBtn.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionBtn> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除权限按钮表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01201")]
        [HttpPost]
        public ApiResult DeletePermissionBtn(Guid ID)
        {
            var model = new PermissionBtn { ID = ID };
            db.PermissionBtn.Attach(model);
            db.PermissionBtn.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部权限按钮表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ButtonName">按钮名称</param>
        /// <param name="FormName">窗体名称</param>
        /// <param name="DisplayName">显示名称</param>
        /// <param name="ApplicationID"></param>
        /// <param name="PermissionGroupID"></param>
        /// <param name="NoPermissionType">无权限时操作方式：禁用，隐藏</param>
        /// <param name="OrderByPermissionBtn">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01201")]
        [HttpGet]
        public ApiResult<PagedList<PermissionBtn>> GetPermissionBtnAll(Guid? ID, string ButtonName, string FormName, string DisplayName, Guid? ApplicationID, Guid? PermissionGroupID, string NoPermissionType, string OrderByPermissionBtn, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionBtnAllSelector(ID, ButtonName, FormName, DisplayName, ApplicationID, PermissionGroupID, NoPermissionType, OrderByPermissionBtn, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionBtn>> { Data = result.Data.CastPagedList<PermissionBtn>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部权限按钮表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ButtonName">按钮名称</param>
        /// <param name="FormName">窗体名称</param>
        /// <param name="DisplayName">显示名称</param>
        /// <param name="ApplicationID"></param>
        /// <param name="PermissionGroupID"></param>
        /// <param name="NoPermissionType">无权限时操作方式：禁用，隐藏</param>
        /// <param name="OrderByPermissionBtn">排序条件</param>
        /// <param name="SelectorPermissionBtn">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01202")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionBtnAllSelector(Guid? ID, string ButtonName, string FormName, string DisplayName, Guid? ApplicationID, Guid? PermissionGroupID, string NoPermissionType, string OrderByPermissionBtn, string SelectorPermissionBtn, int PageIndex, int PageSize)
        {
			var list = db.PermissionBtn.Includes(Utility.GetIncludes(SelectorPermissionBtn));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(ButtonName))
			{
				if(ButtonName.StartsWith("="))
				{
					var value = ButtonName.Substring(1);
					list = list.Where(a => a.ButtonName == value);
				}
				else
				{
					list = list.Where(a => a.ButtonName.Contains(ButtonName));
				}
			}
			if(!string.IsNullOrEmpty(FormName))
			{
				if(FormName.StartsWith("="))
				{
					var value = FormName.Substring(1);
					list = list.Where(a => a.FormName == value);
				}
				else
				{
					list = list.Where(a => a.FormName.Contains(FormName));
				}
			}
			if(!string.IsNullOrEmpty(DisplayName))
			{
				if(DisplayName.StartsWith("="))
				{
					var value = DisplayName.Substring(1);
					list = list.Where(a => a.DisplayName == value);
				}
				else
				{
					list = list.Where(a => a.DisplayName.Contains(DisplayName));
				}
			}
			if(ApplicationID != null)
			{
				list = list.Where(a => a.ApplicationID == ApplicationID);
			}
			if(PermissionGroupID != null)
			{
				list = list.Where(a => a.PermissionGroupID == PermissionGroupID);
			}
			if(!string.IsNullOrEmpty(NoPermissionType))
			{
				if(NoPermissionType.StartsWith("="))
				{
					var value = NoPermissionType.Substring(1);
					list = list.Where(a => a.NoPermissionType == value);
				}
				else
				{
					list = list.Where(a => a.NoPermissionType.Contains(NoPermissionType));
				}
			}

            var selector = Utility.DeserializeSelector<PermissionBtn>(SelectorPermissionBtn);
            var orderByList = Utility.DeserializeOrderBy<PermissionBtn>(OrderByPermissionBtn);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取权限按钮表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01203")]
        [HttpGet]
        public ApiResult<PermissionBtn> GetPermissionBtn(Guid ID)
        {
            var model = db.PermissionBtn.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionBtn> { Data = model };
        }

        #endregion

        #region 权限按钮权限功能关系表

        /// <summary>
        /// 添加/修改权限按钮权限功能关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮权限功能关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01301")]
        [HttpPost]
        public ApiResult<PermissionBtnInterface> EditPermissionBtnInterface(bool IsAdd, PermissionBtnInterface Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionBtnInterface.Add(Model);
            }
            else
            {
                db.PermissionBtnInterface.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionBtnInterface> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除权限按钮权限功能关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01301")]
        [HttpPost]
        public ApiResult DeletePermissionBtnInterface(Guid ID)
        {
            var model = new PermissionBtnInterface { ID = ID };
            db.PermissionBtnInterface.Attach(model);
            db.PermissionBtnInterface.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部权限按钮权限功能关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionButtonID">权限按钮Id</param>
        /// <param name="PermissionInterfaceID">权限功能Id</param>
        /// <param name="OrderByPermissionBtnInterface">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01301")]
        [HttpGet]
        public ApiResult<PagedList<PermissionBtnInterface>> GetPermissionBtnInterfaceAll(Guid? ID, Guid? PermissionButtonID, Guid? PermissionInterfaceID, string OrderByPermissionBtnInterface, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionBtnInterfaceAllSelector(ID, PermissionButtonID, PermissionInterfaceID, OrderByPermissionBtnInterface, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionBtnInterface>> { Data = result.Data.CastPagedList<PermissionBtnInterface>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部权限按钮权限功能关系表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionButtonID">权限按钮Id</param>
        /// <param name="PermissionInterfaceID">权限功能Id</param>
        /// <param name="OrderByPermissionBtnInterface">排序条件</param>
        /// <param name="SelectorPermissionBtnInterface">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01302")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionBtnInterfaceAllSelector(Guid? ID, Guid? PermissionButtonID, Guid? PermissionInterfaceID, string OrderByPermissionBtnInterface, string SelectorPermissionBtnInterface, int PageIndex, int PageSize)
        {
			var list = db.PermissionBtnInterface.Includes(Utility.GetIncludes(SelectorPermissionBtnInterface));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(PermissionButtonID != null)
			{
				list = list.Where(a => a.PermissionButtonID == PermissionButtonID);
			}
			if(PermissionInterfaceID != null)
			{
				list = list.Where(a => a.PermissionInterfaceID == PermissionInterfaceID);
			}

            var selector = Utility.DeserializeSelector<PermissionBtnInterface>(SelectorPermissionBtnInterface);
            var orderByList = Utility.DeserializeOrderBy<PermissionBtnInterface>(OrderByPermissionBtnInterface);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取权限按钮权限功能关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01303")]
        [HttpGet]
        public ApiResult<PermissionBtnInterface> GetPermissionBtnInterface(Guid ID)
        {
            var model = db.PermissionBtnInterface.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionBtnInterface> { Data = model };
        }

        #endregion

        #region 权限按钮角色关系表

        /// <summary>
        /// 添加/修改权限按钮角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01401")]
        [HttpPost]
        public ApiResult<PermissionBtnRole> EditPermissionBtnRole(bool IsAdd, PermissionBtnRole Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionBtnRole.Add(Model);
            }
            else
            {
                db.PermissionBtnRole.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionBtnRole> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除权限按钮角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01401")]
        [HttpPost]
        public ApiResult DeletePermissionBtnRole(Guid ID)
        {
            var model = new PermissionBtnRole { ID = ID };
            db.PermissionBtnRole.Attach(model);
            db.PermissionBtnRole.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部权限按钮角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionButtonID">权限按钮Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByPermissionBtnRole">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01401")]
        [HttpGet]
        public ApiResult<PagedList<PermissionBtnRole>> GetPermissionBtnRoleAll(Guid? ID, Guid? PermissionButtonID, Guid? RoleID, string OrderByPermissionBtnRole, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionBtnRoleAllSelector(ID, PermissionButtonID, RoleID, OrderByPermissionBtnRole, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionBtnRole>> { Data = result.Data.CastPagedList<PermissionBtnRole>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部权限按钮角色关系表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionButtonID">权限按钮Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByPermissionBtnRole">排序条件</param>
        /// <param name="SelectorPermissionBtnRole">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01402")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionBtnRoleAllSelector(Guid? ID, Guid? PermissionButtonID, Guid? RoleID, string OrderByPermissionBtnRole, string SelectorPermissionBtnRole, int PageIndex, int PageSize)
        {
			var list = db.PermissionBtnRole.Includes(Utility.GetIncludes(SelectorPermissionBtnRole));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(PermissionButtonID != null)
			{
				list = list.Where(a => a.PermissionButtonID == PermissionButtonID);
			}
			if(RoleID != null)
			{
				list = list.Where(a => a.RoleID == RoleID);
			}

            var selector = Utility.DeserializeSelector<PermissionBtnRole>(SelectorPermissionBtnRole);
            var orderByList = Utility.DeserializeOrderBy<PermissionBtnRole>(OrderByPermissionBtnRole);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取权限按钮角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01403")]
        [HttpGet]
        public ApiResult<PermissionBtnRole> GetPermissionBtnRole(Guid ID)
        {
            var model = db.PermissionBtnRole.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionBtnRole> { Data = model };
        }

        #endregion

        #region 功能类型表

        /// <summary>
        /// 添加/修改功能类型表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">功能类型表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01501")]
        [HttpPost]
        public ApiResult<PermissionGroup> EditPermissionGroup(bool IsAdd, PermissionGroup Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionGroup.Add(Model);
            }
            else
            {
                db.PermissionGroup.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionGroup> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除功能类型表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01501")]
        [HttpPost]
        public ApiResult DeletePermissionGroup(Guid ID)
        {
            var model = new PermissionGroup { ID = ID };
            db.PermissionGroup.Attach(model);
            db.PermissionGroup.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部功能类型表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">类型名称</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="ParentID">父级Id</param>
        /// <param name="Sort">排序</param>
        /// <param name="IdPath">ID路径</param>
        /// <param name="Path">名称路径</param>
        /// <param name="Description"></param>
        /// <param name="OrderByPermissionGroup">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01501")]
        [HttpGet]
        public ApiResult<PagedList<PermissionGroup>> GetPermissionGroupAll(Guid? ID, string Name, Guid? ApplicationID, Guid? ParentID, int? Sort, string IdPath, string Path, string Description, string OrderByPermissionGroup, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionGroupAllSelector(ID, Name, ApplicationID, ParentID, Sort, IdPath, Path, Description, OrderByPermissionGroup, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionGroup>> { Data = result.Data.CastPagedList<PermissionGroup>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部功能类型表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">类型名称</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="ParentID">父级Id</param>
        /// <param name="Sort">排序</param>
        /// <param name="IdPath">ID路径</param>
        /// <param name="Path">名称路径</param>
        /// <param name="Description"></param>
        /// <param name="OrderByPermissionGroup">排序条件</param>
        /// <param name="SelectorPermissionGroup">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01502")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionGroupAllSelector(Guid? ID, string Name, Guid? ApplicationID, Guid? ParentID, int? Sort, string IdPath, string Path, string Description, string OrderByPermissionGroup, string SelectorPermissionGroup, int PageIndex, int PageSize)
        {
			var list = db.PermissionGroup.Includes(Utility.GetIncludes(SelectorPermissionGroup));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(ApplicationID != null)
			{
				list = list.Where(a => a.ApplicationID == ApplicationID);
			}
			if(ParentID != null)
			{
				list = list.Where(a => a.ParentID == ParentID);
			}
			if(Sort != null)
			{
				list = list.Where(a => a.Sort == Sort);
			}
			if(!string.IsNullOrEmpty(IdPath))
			{
				if(IdPath.StartsWith("="))
				{
					var value = IdPath.Substring(1);
					list = list.Where(a => a.IdPath == value);
				}
				else
				{
					list = list.Where(a => a.IdPath.Contains(IdPath));
				}
			}
			if(!string.IsNullOrEmpty(Path))
			{
				if(Path.StartsWith("="))
				{
					var value = Path.Substring(1);
					list = list.Where(a => a.Path == value);
				}
				else
				{
					list = list.Where(a => a.Path.Contains(Path));
				}
			}
			if(!string.IsNullOrEmpty(Description))
			{
				if(Description.StartsWith("="))
				{
					var value = Description.Substring(1);
					list = list.Where(a => a.Description == value);
				}
				else
				{
					list = list.Where(a => a.Description.Contains(Description));
				}
			}

            var selector = Utility.DeserializeSelector<PermissionGroup>(SelectorPermissionGroup);
            var orderByList = Utility.DeserializeOrderBy<PermissionGroup>(OrderByPermissionGroup);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取功能类型表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01503")]
        [HttpGet]
        public ApiResult<PermissionGroup> GetPermissionGroup(Guid ID)
        {
            var model = db.PermissionGroup.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionGroup> { Data = model };
        }

        #endregion

        #region 权限接口表

        /// <summary>
        /// 添加/修改权限接口表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限接口表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01601")]
        [HttpPost]
        public ApiResult<PermissionInterface> EditPermissionInterface(bool IsAdd, PermissionInterface Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionInterface.Add(Model);
            }
            else
            {
                db.PermissionInterface.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionInterface> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除权限接口表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01601")]
        [HttpPost]
        public ApiResult DeletePermissionInterface(Guid ID)
        {
            var model = new PermissionInterface { ID = ID };
            db.PermissionInterface.Attach(model);
            db.PermissionInterface.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部权限接口表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">名称</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="ActionName">动作名称</param>
        /// <param name="QueryString">参数验证:（?参数名=参数值&参数名=参数值）</param>
        /// <param name="Description"></param>
        /// <param name="OrderByPermissionInterface">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01601")]
        [HttpGet]
        public ApiResult<PagedList<PermissionInterface>> GetPermissionInterfaceAll(Guid? ID, string Name, Guid? ApplicationID, string ControllerName, string MethodName, string ActionName, string QueryString, string Description, string OrderByPermissionInterface, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionInterfaceAllSelector(ID, Name, ApplicationID, ControllerName, MethodName, ActionName, QueryString, Description, OrderByPermissionInterface, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionInterface>> { Data = result.Data.CastPagedList<PermissionInterface>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部权限接口表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">名称</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="ActionName">动作名称</param>
        /// <param name="QueryString">参数验证:（?参数名=参数值&参数名=参数值）</param>
        /// <param name="Description"></param>
        /// <param name="OrderByPermissionInterface">排序条件</param>
        /// <param name="SelectorPermissionInterface">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01602")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionInterfaceAllSelector(Guid? ID, string Name, Guid? ApplicationID, string ControllerName, string MethodName, string ActionName, string QueryString, string Description, string OrderByPermissionInterface, string SelectorPermissionInterface, int PageIndex, int PageSize)
        {
			var list = db.PermissionInterface.Includes(Utility.GetIncludes(SelectorPermissionInterface));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(ApplicationID != null)
			{
				list = list.Where(a => a.ApplicationID == ApplicationID);
			}
			if(!string.IsNullOrEmpty(ControllerName))
			{
				if(ControllerName.StartsWith("="))
				{
					var value = ControllerName.Substring(1);
					list = list.Where(a => a.ControllerName == value);
				}
				else
				{
					list = list.Where(a => a.ControllerName.Contains(ControllerName));
				}
			}
			if(!string.IsNullOrEmpty(MethodName))
			{
				if(MethodName.StartsWith("="))
				{
					var value = MethodName.Substring(1);
					list = list.Where(a => a.MethodName == value);
				}
				else
				{
					list = list.Where(a => a.MethodName.Contains(MethodName));
				}
			}
			if(!string.IsNullOrEmpty(ActionName))
			{
				if(ActionName.StartsWith("="))
				{
					var value = ActionName.Substring(1);
					list = list.Where(a => a.ActionName == value);
				}
				else
				{
					list = list.Where(a => a.ActionName.Contains(ActionName));
				}
			}
			if(!string.IsNullOrEmpty(QueryString))
			{
				if(QueryString.StartsWith("="))
				{
					var value = QueryString.Substring(1);
					list = list.Where(a => a.QueryString == value);
				}
				else
				{
					list = list.Where(a => a.QueryString.Contains(QueryString));
				}
			}
			if(!string.IsNullOrEmpty(Description))
			{
				if(Description.StartsWith("="))
				{
					var value = Description.Substring(1);
					list = list.Where(a => a.Description == value);
				}
				else
				{
					list = list.Where(a => a.Description.Contains(Description));
				}
			}

            var selector = Utility.DeserializeSelector<PermissionInterface>(SelectorPermissionInterface);
            var orderByList = Utility.DeserializeOrderBy<PermissionInterface>(OrderByPermissionInterface);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取权限接口表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01603")]
        [HttpGet]
        public ApiResult<PermissionInterface> GetPermissionInterface(Guid ID)
        {
            var model = db.PermissionInterface.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionInterface> { Data = model };
        }

        #endregion

        #region 接口角色关系表

        /// <summary>
        /// 添加/修改接口角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">接口角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01701")]
        [HttpPost]
        public ApiResult<PermissionInterfaceRole> EditPermissionInterfaceRole(bool IsAdd, PermissionInterfaceRole Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.PermissionInterfaceRole.Add(Model);
            }
            else
            {
                db.PermissionInterfaceRole.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<PermissionInterfaceRole> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除接口角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01701")]
        [HttpPost]
        public ApiResult DeletePermissionInterfaceRole(Guid ID)
        {
            var model = new PermissionInterfaceRole { ID = ID };
            db.PermissionInterfaceRole.Attach(model);
            db.PermissionInterfaceRole.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部接口角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionInterfaceID">功能Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByPermissionInterfaceRole">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01701")]
        [HttpGet]
        public ApiResult<PagedList<PermissionInterfaceRole>> GetPermissionInterfaceRoleAll(Guid? ID, Guid? PermissionInterfaceID, Guid? RoleID, string OrderByPermissionInterfaceRole, string Includes, int PageIndex, int PageSize)
        {
            var result = GetPermissionInterfaceRoleAllSelector(ID, PermissionInterfaceID, RoleID, OrderByPermissionInterfaceRole, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<PermissionInterfaceRole>> { Data = result.Data.CastPagedList<PermissionInterfaceRole>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部接口角色关系表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PermissionInterfaceID">功能Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByPermissionInterfaceRole">排序条件</param>
        /// <param name="SelectorPermissionInterfaceRole">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01702")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetPermissionInterfaceRoleAllSelector(Guid? ID, Guid? PermissionInterfaceID, Guid? RoleID, string OrderByPermissionInterfaceRole, string SelectorPermissionInterfaceRole, int PageIndex, int PageSize)
        {
			var list = db.PermissionInterfaceRole.Includes(Utility.GetIncludes(SelectorPermissionInterfaceRole));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(PermissionInterfaceID != null)
			{
				list = list.Where(a => a.PermissionInterfaceID == PermissionInterfaceID);
			}
			if(RoleID != null)
			{
				list = list.Where(a => a.RoleID == RoleID);
			}

            var selector = Utility.DeserializeSelector<PermissionInterfaceRole>(SelectorPermissionInterfaceRole);
            var orderByList = Utility.DeserializeOrderBy<PermissionInterfaceRole>(OrderByPermissionInterfaceRole);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取接口角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01703")]
        [HttpGet]
        public ApiResult<PermissionInterfaceRole> GetPermissionInterfaceRole(Guid ID)
        {
            var model = db.PermissionInterfaceRole.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<PermissionInterfaceRole> { Data = model };
        }

        #endregion

        #region 角色表

        /// <summary>
        /// 添加/修改角色表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">角色表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01801")]
        [HttpPost]
        public ApiResult<Role> EditRole(bool IsAdd, Role Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.Role.Add(Model);
            }
            else
            {
                db.Role.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<Role> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除角色表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01801")]
        [HttpPost]
        public ApiResult DeleteRole(Guid ID)
        {
            var model = new Role { ID = ID };
            db.Role.Attach(model);
            db.Role.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部角色表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">角色名称</param>
        /// <param name="Description">角色的说明</param>
        /// <param name="OrderByRole">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01801")]
        [HttpGet]
        public ApiResult<PagedList<Role>> GetRoleAll(Guid? ID, string Name, string Description, string OrderByRole, string Includes, int PageIndex, int PageSize)
        {
            var result = GetRoleAllSelector(ID, Name, Description, OrderByRole, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<Role>> { Data = result.Data.CastPagedList<Role>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部角色表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name">角色名称</param>
        /// <param name="Description">角色的说明</param>
        /// <param name="OrderByRole">排序条件</param>
        /// <param name="SelectorRole">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01802")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetRoleAllSelector(Guid? ID, string Name, string Description, string OrderByRole, string SelectorRole, int PageIndex, int PageSize)
        {
			var list = db.Role.Includes(Utility.GetIncludes(SelectorRole));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(!string.IsNullOrEmpty(Description))
			{
				if(Description.StartsWith("="))
				{
					var value = Description.Substring(1);
					list = list.Where(a => a.Description == value);
				}
				else
				{
					list = list.Where(a => a.Description.Contains(Description));
				}
			}

            var selector = Utility.DeserializeSelector<Role>(SelectorRole);
            var orderByList = Utility.DeserializeOrderBy<Role>(OrderByRole);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取角色表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01803")]
        [HttpGet]
        public ApiResult<Role> GetRole(Guid ID)
        {
            var model = db.Role.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<Role> { Data = model };
        }

        #endregion

        #region 用户表

        /// <summary>
        /// 添加/修改用户表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG01901")]
        [HttpPost]
        public ApiResult<User> EditUser(bool IsAdd, User Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.User.Add(Model);
            }
            else
            {
                db.User.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<User> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除用户表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC01901")]
        [HttpPost]
        public ApiResult DeleteUser(Guid ID)
        {
            var model = new User { ID = ID };
            db.User.Attach(model);
            db.User.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部用户表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <param name="Name">名称</param>
        /// <param name="CreateTime">创建时间</param>
        /// <param name="Signature">签名</param>
        /// <param name="Fingerprint">指纹</param>
        /// <param name="FingerprintSpare">指纹备用</param>
        /// <param name="IsDeleted"></param>
        /// <param name="OrderByUser">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01901")]
        [HttpGet]
        public ApiResult<PagedList<User>> GetUserAll(Guid? ID, string Account, string Password, string Name, DateTime? CreateTime, byte[] Signature, byte[] Fingerprint, byte[] FingerprintSpare, int? IsDeleted, string OrderByUser, string Includes, int PageIndex, int PageSize)
        {
            var result = GetUserAllSelector(ID, Account, Password, Name, CreateTime, Signature, Fingerprint, FingerprintSpare, IsDeleted, OrderByUser, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<User>> { Data = result.Data.CastPagedList<User>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部用户表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <param name="Name">名称</param>
        /// <param name="CreateTime">创建时间</param>
        /// <param name="Signature">签名</param>
        /// <param name="Fingerprint">指纹</param>
        /// <param name="FingerprintSpare">指纹备用</param>
        /// <param name="IsDeleted"></param>
        /// <param name="OrderByUser">排序条件</param>
        /// <param name="SelectorUser">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01902")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetUserAllSelector(Guid? ID, string Account, string Password, string Name, DateTime? CreateTime, byte[] Signature, byte[] Fingerprint, byte[] FingerprintSpare, int? IsDeleted, string OrderByUser, string SelectorUser, int PageIndex, int PageSize)
        {
			var list = db.User.Includes(Utility.GetIncludes(SelectorUser));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(!string.IsNullOrEmpty(Account))
			{
				if(Account.StartsWith("="))
				{
					var value = Account.Substring(1);
					list = list.Where(a => a.Account == value);
				}
				else
				{
					list = list.Where(a => a.Account.Contains(Account));
				}
			}
			if(!string.IsNullOrEmpty(Password))
			{
				if(Password.StartsWith("="))
				{
					var value = Password.Substring(1);
					list = list.Where(a => a.Password == value);
				}
				else
				{
					list = list.Where(a => a.Password.Contains(Password));
				}
			}
			if(!string.IsNullOrEmpty(Name))
			{
				if(Name.StartsWith("="))
				{
					var value = Name.Substring(1);
					list = list.Where(a => a.Name == value);
				}
				else
				{
					list = list.Where(a => a.Name.Contains(Name));
				}
			}
			if(CreateTime != null)
			{
				list = list.Where(a => a.CreateTime == CreateTime);
			}
			if(Signature != null)
			{
				list = list.Where(a => a.Signature == Signature);
			}
			if(Fingerprint != null)
			{
				list = list.Where(a => a.Fingerprint == Fingerprint);
			}
			if(FingerprintSpare != null)
			{
				list = list.Where(a => a.FingerprintSpare == FingerprintSpare);
			}
			if(IsDeleted != null)
			{
				list = list.Where(a => a.IsDeleted == IsDeleted);
			}

            var selector = Utility.DeserializeSelector<User>(SelectorUser);
            var orderByList = Utility.DeserializeOrderBy<User>(OrderByUser);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取用户表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX01903")]
        [HttpGet]
        public ApiResult<User> GetUser(Guid ID)
        {
            var model = db.User.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<User> { Data = model };
        }

        #endregion

        #region 用户登录令牌表

        /// <summary>
        /// 添加/修改用户登录令牌表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户登录令牌表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG02001")]
        [HttpPost]
        public ApiResult<UserToken> EditUserToken(bool IsAdd, UserToken Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.UserToken.Add(Model);
            }
            else
            {
                db.UserToken.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<UserToken> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除用户登录令牌表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC02001")]
        [HttpPost]
        public ApiResult DeleteUserToken(Guid ID)
        {
            var model = new UserToken { ID = ID };
            db.UserToken.Attach(model);
            db.UserToken.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部用户登录令牌表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserID">用户Id</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="Token"></param>
        /// <param name="UpdateTime">更新时间</param>
        /// <param name="UsingApplicationID"></param>
        /// <param name="OrderByUserToken">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX02001")]
        [HttpGet]
        public ApiResult<PagedList<UserToken>> GetUserTokenAll(Guid? ID, Guid? UserID, Guid? ApplicationID, string Token, DateTime? UpdateTime, Guid? UsingApplicationID, string OrderByUserToken, string Includes, int PageIndex, int PageSize)
        {
            var result = GetUserTokenAllSelector(ID, UserID, ApplicationID, Token, UpdateTime, UsingApplicationID, OrderByUserToken, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<UserToken>> { Data = result.Data.CastPagedList<UserToken>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部用户登录令牌表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserID">用户Id</param>
        /// <param name="ApplicationID">应用Id</param>
        /// <param name="Token"></param>
        /// <param name="UpdateTime">更新时间</param>
        /// <param name="UsingApplicationID"></param>
        /// <param name="OrderByUserToken">排序条件</param>
        /// <param name="SelectorUserToken">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX02002")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetUserTokenAllSelector(Guid? ID, Guid? UserID, Guid? ApplicationID, string Token, DateTime? UpdateTime, Guid? UsingApplicationID, string OrderByUserToken, string SelectorUserToken, int PageIndex, int PageSize)
        {
			var list = db.UserToken.Includes(Utility.GetIncludes(SelectorUserToken));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(UserID != null)
			{
				list = list.Where(a => a.UserID == UserID);
			}
			if(ApplicationID != null)
			{
				list = list.Where(a => a.ApplicationID == ApplicationID);
			}
			if(!string.IsNullOrEmpty(Token))
			{
				if(Token.StartsWith("="))
				{
					var value = Token.Substring(1);
					list = list.Where(a => a.Token == value);
				}
				else
				{
					list = list.Where(a => a.Token.Contains(Token));
				}
			}
			if(UpdateTime != null)
			{
				list = list.Where(a => a.UpdateTime == UpdateTime);
			}
			if(UsingApplicationID != null)
			{
				list = list.Where(a => a.UsingApplicationID == UsingApplicationID);
			}

            var selector = Utility.DeserializeSelector<UserToken>(SelectorUserToken);
            var orderByList = Utility.DeserializeOrderBy<UserToken>(OrderByUserToken);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取用户登录令牌表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX02003")]
        [HttpGet]
        public ApiResult<UserToken> GetUserToken(Guid ID)
        {
            var model = db.UserToken.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<UserToken> { Data = model };
        }

        #endregion

        #region 用户角色关系表

        /// <summary>
        /// 添加/修改用户角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
        [ActionName("XG02101")]
        [HttpPost]
        public ApiResult<UserToRole> EditUserToRole(bool IsAdd, UserToRole Model)
        {
            if (IsAdd)
            {
				if(Model.ID == Guid.Empty)
				{
					Model.ID = Guid.NewGuid();
				}
                db.UserToRole.Add(Model);
            }
            else
            {
                db.UserToRole.Attach(Model);
                var entry = db.Entry(Model);
                entry.State = EntityState.Modified;
				
                // 不想更新的字段列表
                foreach (var item in new string[] {  })
                {
                    entry.Property(item).IsModified = false;
                }
            }
            db.SaveChanges();
            return new ApiResult<UserToRole> { Message = (IsAdd ? "添加" : "修改") + "成功！", Data = Model };
        }

        /// <summary>
        /// 删除用户角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("SC02101")]
        [HttpPost]
        public ApiResult DeleteUserToRole(Guid ID)
        {
            var model = new UserToRole { ID = ID };
            db.UserToRole.Attach(model);
            db.UserToRole.Remove(model);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获得全部用户角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserID">用户Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByUserToRole">排序条件</param>
        /// <param name="Includes">引用外键</param>
        /// <param name="PageIndex">第几页</param> 
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX02101")]
        [HttpGet]
        public ApiResult<PagedList<UserToRole>> GetUserToRoleAll(Guid? ID, Guid? UserID, Guid? RoleID, string OrderByUserToRole, string Includes, int PageIndex, int PageSize)
        {
            var result = GetUserToRoleAllSelector(ID, UserID, RoleID, OrderByUserToRole, "Includes:" + Includes, PageIndex, PageSize);

            return new ApiResult<PagedList<UserToRole>> { Data = result.Data.CastPagedList<UserToRole>(), ErrorCode = result.ErrorCode, Message = result.Message };
        }

        /// <summary>
        /// 获取全部用户角色关系表，自定义返回类型
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserID">用户Id</param>
        /// <param name="RoleID">角色Id</param>
        /// <param name="OrderByUserToRole">排序条件</param>
        /// <param name="SelectorUserToRole">查询哪些字段</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX02102")]
        [HttpGet]
        public ApiResult<PagedList<object>> GetUserToRoleAllSelector(Guid? ID, Guid? UserID, Guid? RoleID, string OrderByUserToRole, string SelectorUserToRole, int PageIndex, int PageSize)
        {
			var list = db.UserToRole.Includes(Utility.GetIncludes(SelectorUserToRole));

			if(ID != null)
			{
				list = list.Where(a => a.ID == ID);
			}
			if(UserID != null)
			{
				list = list.Where(a => a.UserID == UserID);
			}
			if(RoleID != null)
			{
				list = list.Where(a => a.RoleID == RoleID);
			}

            var selector = Utility.DeserializeSelector<UserToRole>(SelectorUserToRole);
            var orderByList = Utility.DeserializeOrderBy<UserToRole>(OrderByUserToRole);
            if (orderByList.Any())
            {
                list = list.OrderByList(orderByList);
            }
            else
            {
                list = list.OrderBy(a => new { a.ID });
            }

            return new ApiResult<PagedList<object>> { Data = list.SelectObject(selector).ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获取用户角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [ActionName("CX02103")]
        [HttpGet]
        public ApiResult<UserToRole> GetUserToRole(Guid ID)
        {
            var model = db.UserToRole.Where(a => a.ID == ID).FirstOrDefault();
            return new ApiResult<UserToRole> { Data = model };
        }

        #endregion
    }
}

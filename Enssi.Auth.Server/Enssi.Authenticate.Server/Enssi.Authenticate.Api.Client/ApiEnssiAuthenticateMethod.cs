using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;

namespace Enssi
{
	/// <summary>
	/// Api 方法
	/// </summary>
	public class ApiEnssiAuthenticateMethod
	{
		/// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
		public static async Task<ApiResult<Auth_Login_Result>> Auth_Login(string SystemNameSpace = default(string), string UsingNameSpace = default(string), string Account = default(string), string Password = default(string))
		{
			var request = new
			{
				SystemNameSpace = SystemNameSpace,
				UsingNameSpace = UsingNameSpace,
				Account = Account,
				Password = Password,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Auth_Login_Result>>(ApiCall.QueryString("/api/Auth/CX00101", request), null);
		}

		/// <summary>
        /// 获取全部指纹
        /// </summary>
        /// <returns></returns>
		public static async Task<ApiResult<List<FingerprintModel>>> Auth_GetFingerprint()
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<List<FingerprintModel>>>(ApiCall.QueryString("/api/Auth/CX00201", request), null);
		}

		/// <summary>
        /// 指纹登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="UID">用户编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<Auth_Login_Result>> Auth_LoginByFinger(string SystemNameSpace = default(string), string UsingNameSpace = default(string), Guid UID = default(Guid))
		{
			var request = new
			{
				SystemNameSpace = SystemNameSpace,
				UsingNameSpace = UsingNameSpace,
				UID = UID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Auth_Login_Result>>(ApiCall.QueryString("/api/Auth/CX00202", request), null);
		}

		/// <summary>
        /// 扫脸登录
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <param name="UsingNameSpace">使用中系统命名空间</param>
        /// <param name="OrgID">组织架构编号</param>
        /// <param name="KeyCode">扫脸码</param>
        /// <param name="Application">应用编码</param>
        /// <returns></returns>
		public static async Task<ApiResult<Auth_Login_Result>> Auth_LoginScanningFace(string SystemNameSpace = default(string), string UsingNameSpace = default(string), Guid OrgID = default(Guid), string KeyCode = default(string), string Application = default(string))
		{
			var request = new
			{
				SystemNameSpace = SystemNameSpace,
				UsingNameSpace = UsingNameSpace,
				OrgID = OrgID,
				KeyCode = KeyCode,
				Application = Application,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Auth_Login_Result>>(ApiCall.QueryString("/api/Auth/CX00301", request), null);
		}

		/// <summary>
        /// 客户端权限验证，获得当前用户权限按钮验证
        /// </summary>
        /// <param name="SystemNameSpace">系统命名空间</param>
        /// <returns></returns>
		public static async Task<ApiResult<List<PermissionBtnUser>>> Auth_GetCurrentUserPermissionBtnAll(string SystemNameSpace = default(string))
		{
			var request = new
			{
				SystemNameSpace = SystemNameSpace,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<PermissionBtnUser>>>(ApiCall.QueryString("/api/Auth/CX00401", request));
		}

		/// <summary>
        /// 添加/修改应用表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">应用表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<Application>> Authenticate_EditApplication(bool IsAdd = default(bool), Application Model = default(Application))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Application>>(ApiCall.QueryString("/api/Authenticate/XG00101", request), Model);
		}

		/// <summary>
        /// 删除应用表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteApplication(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00101", request), null);
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
		public static async Task<ApiResult<PagedList<Application>>> Authenticate_GetApplicationAll(Guid? ID = default(Guid?), string Name = default(string), string NameSpace = default(string), string Description = default(string), Dictionary<Expression<Func<Application, object>>, string> OrderByApplication = default(Dictionary<Expression<Func<Application, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				NameSpace = NameSpace,
				Description = Description,
				OrderByApplication = OrderByApplication,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Application>>>(ApiCall.QueryString("/api/Authenticate/CX00101", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetApplicationAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), string NameSpace = default(string), string Description = default(string), Dictionary<Expression<Func<Application, object>>, string> OrderByApplication = default(Dictionary<Expression<Func<Application, object>>, string>), Expression<Func<Application, T>> SelectorApplication = default(Expression<Func<Application, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				NameSpace = NameSpace,
				Description = Description,
				OrderByApplication = OrderByApplication,
				SelectorApplication = SelectorApplication,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00102", request));
		}

		/// <summary>
        /// 获取应用表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<Application>> Authenticate_GetApplication(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Application>>(ApiCall.QueryString("/api/Authenticate/CX00103", request));
		}

		/// <summary>
        /// 添加/修改审批日志
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">审批日志实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<ApproveLog>> Authenticate_EditApproveLog(bool IsAdd = default(bool), ApproveLog Model = default(ApproveLog))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<ApproveLog>>(ApiCall.QueryString("/api/Authenticate/XG00201", request), Model);
		}

		/// <summary>
        /// 删除审批日志
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteApproveLog(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00201", request), null);
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
		public static async Task<ApiResult<PagedList<ApproveLog>>> Authenticate_GetApproveLogAll(Guid? ID = default(Guid?), string Keys = default(string), Guid? TemplateID = default(Guid?), Guid? TemplateDetailID = default(Guid?), int? Rank = default(int?), string RankName = default(string), int? Count = default(int?), Guid? ApproveUserID = default(Guid?), string ApproveUserName = default(string), int? Result = default(int?), string ResultState = default(string), string Reason = default(string), DateTime? ApproveTime = default(DateTime?), string HistoryData = default(string), int? NextRank = default(int?), Dictionary<Expression<Func<ApproveLog, object>>, string> OrderByApproveLog = default(Dictionary<Expression<Func<ApproveLog, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Keys = Keys,
				TemplateID = TemplateID,
				TemplateDetailID = TemplateDetailID,
				Rank = Rank,
				RankName = RankName,
				Count = Count,
				ApproveUserID = ApproveUserID,
				ApproveUserName = ApproveUserName,
				Result = Result,
				ResultState = ResultState,
				Reason = Reason,
				ApproveTime = ApproveTime,
				HistoryData = HistoryData,
				NextRank = NextRank,
				OrderByApproveLog = OrderByApproveLog,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<ApproveLog>>>(ApiCall.QueryString("/api/Authenticate/CX00201", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetApproveLogAllSelector<T>(Guid? ID = default(Guid?), string Keys = default(string), Guid? TemplateID = default(Guid?), Guid? TemplateDetailID = default(Guid?), int? Rank = default(int?), string RankName = default(string), int? Count = default(int?), Guid? ApproveUserID = default(Guid?), string ApproveUserName = default(string), int? Result = default(int?), string ResultState = default(string), string Reason = default(string), DateTime? ApproveTime = default(DateTime?), string HistoryData = default(string), int? NextRank = default(int?), Dictionary<Expression<Func<ApproveLog, object>>, string> OrderByApproveLog = default(Dictionary<Expression<Func<ApproveLog, object>>, string>), Expression<Func<ApproveLog, T>> SelectorApproveLog = default(Expression<Func<ApproveLog, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Keys = Keys,
				TemplateID = TemplateID,
				TemplateDetailID = TemplateDetailID,
				Rank = Rank,
				RankName = RankName,
				Count = Count,
				ApproveUserID = ApproveUserID,
				ApproveUserName = ApproveUserName,
				Result = Result,
				ResultState = ResultState,
				Reason = Reason,
				ApproveTime = ApproveTime,
				HistoryData = HistoryData,
				NextRank = NextRank,
				OrderByApproveLog = OrderByApproveLog,
				SelectorApproveLog = SelectorApproveLog,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00202", request));
		}

		/// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult<ApproveLog>> Authenticate_GetApproveLog(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<ApproveLog>>(ApiCall.QueryString("/api/Authenticate/CX00203", request));
		}

		/// <summary>
        /// 添加/修改通用审批模板
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用审批模板实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<ApproveTemplate>> Authenticate_EditApproveTemplate(bool IsAdd = default(bool), ApproveTemplate Model = default(ApproveTemplate))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<ApproveTemplate>>(ApiCall.QueryString("/api/Authenticate/XG00301", request), Model);
		}

		/// <summary>
        /// 删除通用审批模板
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteApproveTemplate(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00301", request), null);
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
		public static async Task<ApiResult<PagedList<ApproveTemplate>>> Authenticate_GetApproveTemplateAll(Guid? ID = default(Guid?), string SystemName = default(string), string ModuleName = default(string), string Remark = default(string), string PassState = default(string), Dictionary<Expression<Func<ApproveTemplate, object>>, string> OrderByApproveTemplate = default(Dictionary<Expression<Func<ApproveTemplate, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				SystemName = SystemName,
				ModuleName = ModuleName,
				Remark = Remark,
				PassState = PassState,
				OrderByApproveTemplate = OrderByApproveTemplate,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<ApproveTemplate>>>(ApiCall.QueryString("/api/Authenticate/CX00301", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetApproveTemplateAllSelector<T>(Guid? ID = default(Guid?), string SystemName = default(string), string ModuleName = default(string), string Remark = default(string), string PassState = default(string), Dictionary<Expression<Func<ApproveTemplate, object>>, string> OrderByApproveTemplate = default(Dictionary<Expression<Func<ApproveTemplate, object>>, string>), Expression<Func<ApproveTemplate, T>> SelectorApproveTemplate = default(Expression<Func<ApproveTemplate, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				SystemName = SystemName,
				ModuleName = ModuleName,
				Remark = Remark,
				PassState = PassState,
				OrderByApproveTemplate = OrderByApproveTemplate,
				SelectorApproveTemplate = SelectorApproveTemplate,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00302", request));
		}

		/// <summary>
        /// 获取通用审批模板
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult<ApproveTemplate>> Authenticate_GetApproveTemplate(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<ApproveTemplate>>(ApiCall.QueryString("/api/Authenticate/CX00303", request));
		}

		/// <summary>
        /// 添加/修改通用审批模板明细
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用审批模板明细实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<ApproveTemplateDetail>> Authenticate_EditApproveTemplateDetail(bool IsAdd = default(bool), ApproveTemplateDetail Model = default(ApproveTemplateDetail))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<ApproveTemplateDetail>>(ApiCall.QueryString("/api/Authenticate/XG00401", request), Model);
		}

		/// <summary>
        /// 删除通用审批模板明细
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteApproveTemplateDetail(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00401", request), null);
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
		public static async Task<ApiResult<PagedList<ApproveTemplateDetail>>> Authenticate_GetApproveTemplateDetailAll(Guid? ID = default(Guid?), Guid? TemplateID = default(Guid?), int? Rank = default(int?), string RankName = default(string), int? PassRank = default(int?), string PassState = default(string), int? RejectRank = default(int?), string RejectState = default(string), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<ApproveTemplateDetail, object>>, string> OrderByApproveTemplateDetail = default(Dictionary<Expression<Func<ApproveTemplateDetail, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				TemplateID = TemplateID,
				Rank = Rank,
				RankName = RankName,
				PassRank = PassRank,
				PassState = PassState,
				RejectRank = RejectRank,
				RejectState = RejectState,
				RoleID = RoleID,
				OrderByApproveTemplateDetail = OrderByApproveTemplateDetail,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<ApproveTemplateDetail>>>(ApiCall.QueryString("/api/Authenticate/CX00401", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetApproveTemplateDetailAllSelector<T>(Guid? ID = default(Guid?), Guid? TemplateID = default(Guid?), int? Rank = default(int?), string RankName = default(string), int? PassRank = default(int?), string PassState = default(string), int? RejectRank = default(int?), string RejectState = default(string), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<ApproveTemplateDetail, object>>, string> OrderByApproveTemplateDetail = default(Dictionary<Expression<Func<ApproveTemplateDetail, object>>, string>), Expression<Func<ApproveTemplateDetail, T>> SelectorApproveTemplateDetail = default(Expression<Func<ApproveTemplateDetail, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				TemplateID = TemplateID,
				Rank = Rank,
				RankName = RankName,
				PassRank = PassRank,
				PassState = PassState,
				RejectRank = RejectRank,
				RejectState = RejectState,
				RoleID = RoleID,
				OrderByApproveTemplateDetail = OrderByApproveTemplateDetail,
				SelectorApproveTemplateDetail = SelectorApproveTemplateDetail,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00402", request));
		}

		/// <summary>
        /// 获取通用审批模板明细
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
		public static async Task<ApiResult<ApproveTemplateDetail>> Authenticate_GetApproveTemplateDetail(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<ApproveTemplateDetail>>(ApiCall.QueryString("/api/Authenticate/CX00403", request));
		}

		/// <summary>
        /// 添加/修改CommonWords
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">CommonWords实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<CommonWords>> Authenticate_EditCommonWords(bool IsAdd = default(bool), CommonWords Model = default(CommonWords))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<CommonWords>>(ApiCall.QueryString("/api/Authenticate/XG00501", request), Model);
		}

		/// <summary>
        /// 删除CommonWords
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteCommonWords(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00501", request), null);
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
		public static async Task<ApiResult<PagedList<CommonWords>>> Authenticate_GetCommonWordsAll(Guid? ID = default(Guid?), string CommonID = default(string), string CommonNo = default(string), string CommonName = default(string), string Remark = default(string), Dictionary<Expression<Func<CommonWords, object>>, string> OrderByCommonWords = default(Dictionary<Expression<Func<CommonWords, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				CommonID = CommonID,
				CommonNo = CommonNo,
				CommonName = CommonName,
				Remark = Remark,
				OrderByCommonWords = OrderByCommonWords,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<CommonWords>>>(ApiCall.QueryString("/api/Authenticate/CX00501", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetCommonWordsAllSelector<T>(Guid? ID = default(Guid?), string CommonID = default(string), string CommonNo = default(string), string CommonName = default(string), string Remark = default(string), Dictionary<Expression<Func<CommonWords, object>>, string> OrderByCommonWords = default(Dictionary<Expression<Func<CommonWords, object>>, string>), Expression<Func<CommonWords, T>> SelectorCommonWords = default(Expression<Func<CommonWords, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				CommonID = CommonID,
				CommonNo = CommonNo,
				CommonName = CommonName,
				Remark = Remark,
				OrderByCommonWords = OrderByCommonWords,
				SelectorCommonWords = SelectorCommonWords,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00502", request));
		}

		/// <summary>
        /// 获取CommonWords
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<CommonWords>> Authenticate_GetCommonWords(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<CommonWords>>(ApiCall.QueryString("/api/Authenticate/CX00503", request));
		}

		/// <summary>
        /// 添加/修改InterfaceDetail
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceDetail实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<InterfaceDetail>> Authenticate_EditInterfaceDetail(bool IsAdd = default(bool), InterfaceDetail Model = default(InterfaceDetail))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<InterfaceDetail>>(ApiCall.QueryString("/api/Authenticate/XG00601", request), Model);
		}

		/// <summary>
        /// 删除InterfaceDetail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteInterfaceDetail(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00601", request), null);
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
		public static async Task<ApiResult<PagedList<InterfaceDetail>>> Authenticate_GetInterfaceDetailAll(Guid? ID = default(Guid?), Guid? InterfaceID = default(Guid?), string ParameterNo = default(string), string ParameterName = default(string), string ParameterType = default(string), int? ParameterLen = default(int?), int? IsNull = default(int?), Dictionary<Expression<Func<InterfaceDetail, object>>, string> OrderByInterfaceDetail = default(Dictionary<Expression<Func<InterfaceDetail, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				InterfaceID = InterfaceID,
				ParameterNo = ParameterNo,
				ParameterName = ParameterName,
				ParameterType = ParameterType,
				ParameterLen = ParameterLen,
				IsNull = IsNull,
				OrderByInterfaceDetail = OrderByInterfaceDetail,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceDetail>>>(ApiCall.QueryString("/api/Authenticate/CX00601", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetInterfaceDetailAllSelector<T>(Guid? ID = default(Guid?), Guid? InterfaceID = default(Guid?), string ParameterNo = default(string), string ParameterName = default(string), string ParameterType = default(string), int? ParameterLen = default(int?), int? IsNull = default(int?), Dictionary<Expression<Func<InterfaceDetail, object>>, string> OrderByInterfaceDetail = default(Dictionary<Expression<Func<InterfaceDetail, object>>, string>), Expression<Func<InterfaceDetail, T>> SelectorInterfaceDetail = default(Expression<Func<InterfaceDetail, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				InterfaceID = InterfaceID,
				ParameterNo = ParameterNo,
				ParameterName = ParameterName,
				ParameterType = ParameterType,
				ParameterLen = ParameterLen,
				IsNull = IsNull,
				OrderByInterfaceDetail = OrderByInterfaceDetail,
				SelectorInterfaceDetail = SelectorInterfaceDetail,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00602", request));
		}

		/// <summary>
        /// 获取InterfaceDetail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceDetail>> Authenticate_GetInterfaceDetail(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceDetail>>(ApiCall.QueryString("/api/Authenticate/CX00603", request));
		}

		/// <summary>
        /// 添加/修改InterfaceDetailHist
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceDetailHist实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<InterfaceDetailHist>> Authenticate_EditInterfaceDetailHist(bool IsAdd = default(bool), InterfaceDetailHist Model = default(InterfaceDetailHist))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<InterfaceDetailHist>>(ApiCall.QueryString("/api/Authenticate/XG00701", request), Model);
		}

		/// <summary>
        /// 删除InterfaceDetailHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteInterfaceDetailHist(Guid PK = default(Guid))
		{
			var request = new
			{
				PK = PK,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00701", request), null);
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
		public static async Task<ApiResult<PagedList<InterfaceDetailHist>>> Authenticate_GetInterfaceDetailHistAll(Guid? PK = default(Guid?), Guid? ID = default(Guid?), Guid? InterfaceID = default(Guid?), string ParameterNo = default(string), string ParameterName = default(string), string ParameterType = default(string), int? ParameterLen = default(int?), int? IsNull = default(int?), string Flag = default(string), Dictionary<Expression<Func<InterfaceDetailHist, object>>, string> OrderByInterfaceDetailHist = default(Dictionary<Expression<Func<InterfaceDetailHist, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PK = PK,
				ID = ID,
				InterfaceID = InterfaceID,
				ParameterNo = ParameterNo,
				ParameterName = ParameterName,
				ParameterType = ParameterType,
				ParameterLen = ParameterLen,
				IsNull = IsNull,
				Flag = Flag,
				OrderByInterfaceDetailHist = OrderByInterfaceDetailHist,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceDetailHist>>>(ApiCall.QueryString("/api/Authenticate/CX00701", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetInterfaceDetailHistAllSelector<T>(Guid? PK = default(Guid?), Guid? ID = default(Guid?), Guid? InterfaceID = default(Guid?), string ParameterNo = default(string), string ParameterName = default(string), string ParameterType = default(string), int? ParameterLen = default(int?), int? IsNull = default(int?), string Flag = default(string), Dictionary<Expression<Func<InterfaceDetailHist, object>>, string> OrderByInterfaceDetailHist = default(Dictionary<Expression<Func<InterfaceDetailHist, object>>, string>), Expression<Func<InterfaceDetailHist, T>> SelectorInterfaceDetailHist = default(Expression<Func<InterfaceDetailHist, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PK = PK,
				ID = ID,
				InterfaceID = InterfaceID,
				ParameterNo = ParameterNo,
				ParameterName = ParameterName,
				ParameterType = ParameterType,
				ParameterLen = ParameterLen,
				IsNull = IsNull,
				Flag = Flag,
				OrderByInterfaceDetailHist = OrderByInterfaceDetailHist,
				SelectorInterfaceDetailHist = SelectorInterfaceDetailHist,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00702", request));
		}

		/// <summary>
        /// 获取InterfaceDetailHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceDetailHist>> Authenticate_GetInterfaceDetailHist(Guid PK = default(Guid))
		{
			var request = new
			{
				PK = PK,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceDetailHist>>(ApiCall.QueryString("/api/Authenticate/CX00703", request));
		}

		/// <summary>
        /// 添加/修改InterfaceMain
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceMain实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<InterfaceMain>> Authenticate_EditInterfaceMain(bool IsAdd = default(bool), InterfaceMain Model = default(InterfaceMain))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<InterfaceMain>>(ApiCall.QueryString("/api/Authenticate/XG00801", request), Model);
		}

		/// <summary>
        /// 删除InterfaceMain
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteInterfaceMain(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00801", request), null);
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
		public static async Task<ApiResult<PagedList<InterfaceMain>>> Authenticate_GetInterfaceMainAll(Guid? ID = default(Guid?), string InterfaceNo = default(string), string BelongSystem = default(string), string BelongModule = default(string), string Operation = default(string), string InExample = default(string), string OutExample = default(string), string Parameter = default(string), string Remark = default(string), int? IsDeleted = default(int?), Guid? CreateUserID = default(Guid?), DateTime? CreateDate = default(DateTime?), Dictionary<Expression<Func<InterfaceMain, object>>, string> OrderByInterfaceMain = default(Dictionary<Expression<Func<InterfaceMain, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				InterfaceNo = InterfaceNo,
				BelongSystem = BelongSystem,
				BelongModule = BelongModule,
				Operation = Operation,
				InExample = InExample,
				OutExample = OutExample,
				Parameter = Parameter,
				Remark = Remark,
				IsDeleted = IsDeleted,
				CreateUserID = CreateUserID,
				CreateDate = CreateDate,
				OrderByInterfaceMain = OrderByInterfaceMain,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceMain>>>(ApiCall.QueryString("/api/Authenticate/CX00801", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetInterfaceMainAllSelector<T>(Guid? ID = default(Guid?), string InterfaceNo = default(string), string BelongSystem = default(string), string BelongModule = default(string), string Operation = default(string), string InExample = default(string), string OutExample = default(string), string Parameter = default(string), string Remark = default(string), int? IsDeleted = default(int?), Guid? CreateUserID = default(Guid?), DateTime? CreateDate = default(DateTime?), Dictionary<Expression<Func<InterfaceMain, object>>, string> OrderByInterfaceMain = default(Dictionary<Expression<Func<InterfaceMain, object>>, string>), Expression<Func<InterfaceMain, T>> SelectorInterfaceMain = default(Expression<Func<InterfaceMain, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				InterfaceNo = InterfaceNo,
				BelongSystem = BelongSystem,
				BelongModule = BelongModule,
				Operation = Operation,
				InExample = InExample,
				OutExample = OutExample,
				Parameter = Parameter,
				Remark = Remark,
				IsDeleted = IsDeleted,
				CreateUserID = CreateUserID,
				CreateDate = CreateDate,
				OrderByInterfaceMain = OrderByInterfaceMain,
				SelectorInterfaceMain = SelectorInterfaceMain,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00802", request));
		}

		/// <summary>
        /// 获取InterfaceMain
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceMain>> Authenticate_GetInterfaceMain(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceMain>>(ApiCall.QueryString("/api/Authenticate/CX00803", request));
		}

		/// <summary>
        /// 添加/修改InterfaceMainHist
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">InterfaceMainHist实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<InterfaceMainHist>> Authenticate_EditInterfaceMainHist(bool IsAdd = default(bool), InterfaceMainHist Model = default(InterfaceMainHist))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<InterfaceMainHist>>(ApiCall.QueryString("/api/Authenticate/XG00901", request), Model);
		}

		/// <summary>
        /// 删除InterfaceMainHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteInterfaceMainHist(Guid PK = default(Guid))
		{
			var request = new
			{
				PK = PK,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC00901", request), null);
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
		public static async Task<ApiResult<PagedList<InterfaceMainHist>>> Authenticate_GetInterfaceMainHistAll(Guid? PK = default(Guid?), Guid? ID = default(Guid?), string InterfaceNo = default(string), string BelongSystem = default(string), string BelongModule = default(string), string Operation = default(string), string InExample = default(string), string OutExample = default(string), string Parameter = default(string), string Remark = default(string), int? IsDeleted = default(int?), Guid? CreateUserID = default(Guid?), DateTime? CreateDate = default(DateTime?), string Flag = default(string), Dictionary<Expression<Func<InterfaceMainHist, object>>, string> OrderByInterfaceMainHist = default(Dictionary<Expression<Func<InterfaceMainHist, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PK = PK,
				ID = ID,
				InterfaceNo = InterfaceNo,
				BelongSystem = BelongSystem,
				BelongModule = BelongModule,
				Operation = Operation,
				InExample = InExample,
				OutExample = OutExample,
				Parameter = Parameter,
				Remark = Remark,
				IsDeleted = IsDeleted,
				CreateUserID = CreateUserID,
				CreateDate = CreateDate,
				Flag = Flag,
				OrderByInterfaceMainHist = OrderByInterfaceMainHist,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceMainHist>>>(ApiCall.QueryString("/api/Authenticate/CX00901", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetInterfaceMainHistAllSelector<T>(Guid? PK = default(Guid?), Guid? ID = default(Guid?), string InterfaceNo = default(string), string BelongSystem = default(string), string BelongModule = default(string), string Operation = default(string), string InExample = default(string), string OutExample = default(string), string Parameter = default(string), string Remark = default(string), int? IsDeleted = default(int?), Guid? CreateUserID = default(Guid?), DateTime? CreateDate = default(DateTime?), string Flag = default(string), Dictionary<Expression<Func<InterfaceMainHist, object>>, string> OrderByInterfaceMainHist = default(Dictionary<Expression<Func<InterfaceMainHist, object>>, string>), Expression<Func<InterfaceMainHist, T>> SelectorInterfaceMainHist = default(Expression<Func<InterfaceMainHist, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PK = PK,
				ID = ID,
				InterfaceNo = InterfaceNo,
				BelongSystem = BelongSystem,
				BelongModule = BelongModule,
				Operation = Operation,
				InExample = InExample,
				OutExample = OutExample,
				Parameter = Parameter,
				Remark = Remark,
				IsDeleted = IsDeleted,
				CreateUserID = CreateUserID,
				CreateDate = CreateDate,
				Flag = Flag,
				OrderByInterfaceMainHist = OrderByInterfaceMainHist,
				SelectorInterfaceMainHist = SelectorInterfaceMainHist,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX00902", request));
		}

		/// <summary>
        /// 获取InterfaceMainHist
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceMainHist>> Authenticate_GetInterfaceMainHist(Guid PK = default(Guid))
		{
			var request = new
			{
				PK = PK,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceMainHist>>(ApiCall.QueryString("/api/Authenticate/CX00903", request));
		}

		/// <summary>
        /// 添加/修改组织架构表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">组织架构表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<Organization>> Authenticate_EditOrganization(bool IsAdd = default(bool), Organization Model = default(Organization))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Organization>>(ApiCall.QueryString("/api/Authenticate/XG01001", request), Model);
		}

		/// <summary>
        /// 删除组织架构表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteOrganization(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01001", request), null);
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
		public static async Task<ApiResult<PagedList<Organization>>> Authenticate_GetOrganizationAll(Guid? ID = default(Guid?), string Name = default(string), string Type = default(string), Guid? ParentID = default(Guid?), int? Sort = default(int?), string Path = default(string), string IdPath = default(string), Guid? GeneralID = default(Guid?), Dictionary<Expression<Func<Organization, object>>, string> OrderByOrganization = default(Dictionary<Expression<Func<Organization, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Type = Type,
				ParentID = ParentID,
				Sort = Sort,
				Path = Path,
				IdPath = IdPath,
				GeneralID = GeneralID,
				OrderByOrganization = OrderByOrganization,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Organization>>>(ApiCall.QueryString("/api/Authenticate/CX01001", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetOrganizationAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), string Type = default(string), Guid? ParentID = default(Guid?), int? Sort = default(int?), string Path = default(string), string IdPath = default(string), Guid? GeneralID = default(Guid?), Dictionary<Expression<Func<Organization, object>>, string> OrderByOrganization = default(Dictionary<Expression<Func<Organization, object>>, string>), Expression<Func<Organization, T>> SelectorOrganization = default(Expression<Func<Organization, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Type = Type,
				ParentID = ParentID,
				Sort = Sort,
				Path = Path,
				IdPath = IdPath,
				GeneralID = GeneralID,
				OrderByOrganization = OrderByOrganization,
				SelectorOrganization = SelectorOrganization,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01002", request));
		}

		/// <summary>
        /// 获取组织架构表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<Organization>> Authenticate_GetOrganization(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Organization>>(ApiCall.QueryString("/api/Authenticate/CX01003", request));
		}

		/// <summary>
        /// 添加/修改通用组织
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">通用组织实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<OrganizationGeneral>> Authenticate_EditOrganizationGeneral(bool IsAdd = default(bool), OrganizationGeneral Model = default(OrganizationGeneral))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<OrganizationGeneral>>(ApiCall.QueryString("/api/Authenticate/XG01101", request), Model);
		}

		/// <summary>
        /// 删除通用组织
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteOrganizationGeneral(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01101", request), null);
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
		public static async Task<ApiResult<PagedList<OrganizationGeneral>>> Authenticate_GetOrganizationGeneralAll(Guid? ID = default(Guid?), string Name = default(string), string Type = default(string), string Description = default(string), string Group = default(string), Dictionary<Expression<Func<OrganizationGeneral, object>>, string> OrderByOrganizationGeneral = default(Dictionary<Expression<Func<OrganizationGeneral, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Type = Type,
				Description = Description,
				Group = Group,
				OrderByOrganizationGeneral = OrderByOrganizationGeneral,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<OrganizationGeneral>>>(ApiCall.QueryString("/api/Authenticate/CX01101", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetOrganizationGeneralAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), string Type = default(string), string Description = default(string), string Group = default(string), Dictionary<Expression<Func<OrganizationGeneral, object>>, string> OrderByOrganizationGeneral = default(Dictionary<Expression<Func<OrganizationGeneral, object>>, string>), Expression<Func<OrganizationGeneral, T>> SelectorOrganizationGeneral = default(Expression<Func<OrganizationGeneral, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Type = Type,
				Description = Description,
				Group = Group,
				OrderByOrganizationGeneral = OrderByOrganizationGeneral,
				SelectorOrganizationGeneral = SelectorOrganizationGeneral,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01102", request));
		}

		/// <summary>
        /// 获取通用组织
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<OrganizationGeneral>> Authenticate_GetOrganizationGeneral(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<OrganizationGeneral>>(ApiCall.QueryString("/api/Authenticate/CX01103", request));
		}

		/// <summary>
        /// 添加/修改权限按钮表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionBtn>> Authenticate_EditPermissionBtn(bool IsAdd = default(bool), PermissionBtn Model = default(PermissionBtn))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionBtn>>(ApiCall.QueryString("/api/Authenticate/XG01201", request), Model);
		}

		/// <summary>
        /// 删除权限按钮表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionBtn(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01201", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionBtn>>> Authenticate_GetPermissionBtnAll(Guid? ID = default(Guid?), string ButtonName = default(string), string FormName = default(string), string DisplayName = default(string), Guid? ApplicationID = default(Guid?), Guid? PermissionGroupID = default(Guid?), string NoPermissionType = default(string), Dictionary<Expression<Func<PermissionBtn, object>>, string> OrderByPermissionBtn = default(Dictionary<Expression<Func<PermissionBtn, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				ButtonName = ButtonName,
				FormName = FormName,
				DisplayName = DisplayName,
				ApplicationID = ApplicationID,
				PermissionGroupID = PermissionGroupID,
				NoPermissionType = NoPermissionType,
				OrderByPermissionBtn = OrderByPermissionBtn,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionBtn>>>(ApiCall.QueryString("/api/Authenticate/CX01201", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionBtnAllSelector<T>(Guid? ID = default(Guid?), string ButtonName = default(string), string FormName = default(string), string DisplayName = default(string), Guid? ApplicationID = default(Guid?), Guid? PermissionGroupID = default(Guid?), string NoPermissionType = default(string), Dictionary<Expression<Func<PermissionBtn, object>>, string> OrderByPermissionBtn = default(Dictionary<Expression<Func<PermissionBtn, object>>, string>), Expression<Func<PermissionBtn, T>> SelectorPermissionBtn = default(Expression<Func<PermissionBtn, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				ButtonName = ButtonName,
				FormName = FormName,
				DisplayName = DisplayName,
				ApplicationID = ApplicationID,
				PermissionGroupID = PermissionGroupID,
				NoPermissionType = NoPermissionType,
				OrderByPermissionBtn = OrderByPermissionBtn,
				SelectorPermissionBtn = SelectorPermissionBtn,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01202", request));
		}

		/// <summary>
        /// 获取权限按钮表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionBtn>> Authenticate_GetPermissionBtn(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionBtn>>(ApiCall.QueryString("/api/Authenticate/CX01203", request));
		}

		/// <summary>
        /// 添加/修改权限按钮权限功能关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮权限功能关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionBtnInterface>> Authenticate_EditPermissionBtnInterface(bool IsAdd = default(bool), PermissionBtnInterface Model = default(PermissionBtnInterface))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionBtnInterface>>(ApiCall.QueryString("/api/Authenticate/XG01301", request), Model);
		}

		/// <summary>
        /// 删除权限按钮权限功能关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionBtnInterface(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01301", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionBtnInterface>>> Authenticate_GetPermissionBtnInterfaceAll(Guid? ID = default(Guid?), Guid? PermissionButtonID = default(Guid?), Guid? PermissionInterfaceID = default(Guid?), Dictionary<Expression<Func<PermissionBtnInterface, object>>, string> OrderByPermissionBtnInterface = default(Dictionary<Expression<Func<PermissionBtnInterface, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionButtonID = PermissionButtonID,
				PermissionInterfaceID = PermissionInterfaceID,
				OrderByPermissionBtnInterface = OrderByPermissionBtnInterface,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionBtnInterface>>>(ApiCall.QueryString("/api/Authenticate/CX01301", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionBtnInterfaceAllSelector<T>(Guid? ID = default(Guid?), Guid? PermissionButtonID = default(Guid?), Guid? PermissionInterfaceID = default(Guid?), Dictionary<Expression<Func<PermissionBtnInterface, object>>, string> OrderByPermissionBtnInterface = default(Dictionary<Expression<Func<PermissionBtnInterface, object>>, string>), Expression<Func<PermissionBtnInterface, T>> SelectorPermissionBtnInterface = default(Expression<Func<PermissionBtnInterface, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionButtonID = PermissionButtonID,
				PermissionInterfaceID = PermissionInterfaceID,
				OrderByPermissionBtnInterface = OrderByPermissionBtnInterface,
				SelectorPermissionBtnInterface = SelectorPermissionBtnInterface,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01302", request));
		}

		/// <summary>
        /// 获取权限按钮权限功能关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionBtnInterface>> Authenticate_GetPermissionBtnInterface(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionBtnInterface>>(ApiCall.QueryString("/api/Authenticate/CX01303", request));
		}

		/// <summary>
        /// 添加/修改权限按钮角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限按钮角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionBtnRole>> Authenticate_EditPermissionBtnRole(bool IsAdd = default(bool), PermissionBtnRole Model = default(PermissionBtnRole))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionBtnRole>>(ApiCall.QueryString("/api/Authenticate/XG01401", request), Model);
		}

		/// <summary>
        /// 删除权限按钮角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionBtnRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01401", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionBtnRole>>> Authenticate_GetPermissionBtnRoleAll(Guid? ID = default(Guid?), Guid? PermissionButtonID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<PermissionBtnRole, object>>, string> OrderByPermissionBtnRole = default(Dictionary<Expression<Func<PermissionBtnRole, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionButtonID = PermissionButtonID,
				RoleID = RoleID,
				OrderByPermissionBtnRole = OrderByPermissionBtnRole,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionBtnRole>>>(ApiCall.QueryString("/api/Authenticate/CX01401", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionBtnRoleAllSelector<T>(Guid? ID = default(Guid?), Guid? PermissionButtonID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<PermissionBtnRole, object>>, string> OrderByPermissionBtnRole = default(Dictionary<Expression<Func<PermissionBtnRole, object>>, string>), Expression<Func<PermissionBtnRole, T>> SelectorPermissionBtnRole = default(Expression<Func<PermissionBtnRole, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionButtonID = PermissionButtonID,
				RoleID = RoleID,
				OrderByPermissionBtnRole = OrderByPermissionBtnRole,
				SelectorPermissionBtnRole = SelectorPermissionBtnRole,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01402", request));
		}

		/// <summary>
        /// 获取权限按钮角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionBtnRole>> Authenticate_GetPermissionBtnRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionBtnRole>>(ApiCall.QueryString("/api/Authenticate/CX01403", request));
		}

		/// <summary>
        /// 添加/修改功能类型表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">功能类型表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionGroup>> Authenticate_EditPermissionGroup(bool IsAdd = default(bool), PermissionGroup Model = default(PermissionGroup))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionGroup>>(ApiCall.QueryString("/api/Authenticate/XG01501", request), Model);
		}

		/// <summary>
        /// 删除功能类型表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionGroup(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01501", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionGroup>>> Authenticate_GetPermissionGroupAll(Guid? ID = default(Guid?), string Name = default(string), Guid? ApplicationID = default(Guid?), Guid? ParentID = default(Guid?), int? Sort = default(int?), string IdPath = default(string), string Path = default(string), string Description = default(string), Dictionary<Expression<Func<PermissionGroup, object>>, string> OrderByPermissionGroup = default(Dictionary<Expression<Func<PermissionGroup, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				ApplicationID = ApplicationID,
				ParentID = ParentID,
				Sort = Sort,
				IdPath = IdPath,
				Path = Path,
				Description = Description,
				OrderByPermissionGroup = OrderByPermissionGroup,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionGroup>>>(ApiCall.QueryString("/api/Authenticate/CX01501", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionGroupAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), Guid? ApplicationID = default(Guid?), Guid? ParentID = default(Guid?), int? Sort = default(int?), string IdPath = default(string), string Path = default(string), string Description = default(string), Dictionary<Expression<Func<PermissionGroup, object>>, string> OrderByPermissionGroup = default(Dictionary<Expression<Func<PermissionGroup, object>>, string>), Expression<Func<PermissionGroup, T>> SelectorPermissionGroup = default(Expression<Func<PermissionGroup, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				ApplicationID = ApplicationID,
				ParentID = ParentID,
				Sort = Sort,
				IdPath = IdPath,
				Path = Path,
				Description = Description,
				OrderByPermissionGroup = OrderByPermissionGroup,
				SelectorPermissionGroup = SelectorPermissionGroup,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01502", request));
		}

		/// <summary>
        /// 获取功能类型表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionGroup>> Authenticate_GetPermissionGroup(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionGroup>>(ApiCall.QueryString("/api/Authenticate/CX01503", request));
		}

		/// <summary>
        /// 添加/修改权限接口表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">权限接口表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionInterface>> Authenticate_EditPermissionInterface(bool IsAdd = default(bool), PermissionInterface Model = default(PermissionInterface))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionInterface>>(ApiCall.QueryString("/api/Authenticate/XG01601", request), Model);
		}

		/// <summary>
        /// 删除权限接口表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionInterface(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01601", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionInterface>>> Authenticate_GetPermissionInterfaceAll(Guid? ID = default(Guid?), string Name = default(string), Guid? ApplicationID = default(Guid?), string ControllerName = default(string), string MethodName = default(string), string ActionName = default(string), string QueryString = default(string), string Description = default(string), Dictionary<Expression<Func<PermissionInterface, object>>, string> OrderByPermissionInterface = default(Dictionary<Expression<Func<PermissionInterface, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				ApplicationID = ApplicationID,
				ControllerName = ControllerName,
				MethodName = MethodName,
				ActionName = ActionName,
				QueryString = QueryString,
				Description = Description,
				OrderByPermissionInterface = OrderByPermissionInterface,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionInterface>>>(ApiCall.QueryString("/api/Authenticate/CX01601", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionInterfaceAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), Guid? ApplicationID = default(Guid?), string ControllerName = default(string), string MethodName = default(string), string ActionName = default(string), string QueryString = default(string), string Description = default(string), Dictionary<Expression<Func<PermissionInterface, object>>, string> OrderByPermissionInterface = default(Dictionary<Expression<Func<PermissionInterface, object>>, string>), Expression<Func<PermissionInterface, T>> SelectorPermissionInterface = default(Expression<Func<PermissionInterface, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				ApplicationID = ApplicationID,
				ControllerName = ControllerName,
				MethodName = MethodName,
				ActionName = ActionName,
				QueryString = QueryString,
				Description = Description,
				OrderByPermissionInterface = OrderByPermissionInterface,
				SelectorPermissionInterface = SelectorPermissionInterface,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01602", request));
		}

		/// <summary>
        /// 获取权限接口表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionInterface>> Authenticate_GetPermissionInterface(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionInterface>>(ApiCall.QueryString("/api/Authenticate/CX01603", request));
		}

		/// <summary>
        /// 添加/修改接口角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">接口角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<PermissionInterfaceRole>> Authenticate_EditPermissionInterfaceRole(bool IsAdd = default(bool), PermissionInterfaceRole Model = default(PermissionInterfaceRole))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<PermissionInterfaceRole>>(ApiCall.QueryString("/api/Authenticate/XG01701", request), Model);
		}

		/// <summary>
        /// 删除接口角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeletePermissionInterfaceRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01701", request), null);
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
		public static async Task<ApiResult<PagedList<PermissionInterfaceRole>>> Authenticate_GetPermissionInterfaceRoleAll(Guid? ID = default(Guid?), Guid? PermissionInterfaceID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<PermissionInterfaceRole, object>>, string> OrderByPermissionInterfaceRole = default(Dictionary<Expression<Func<PermissionInterfaceRole, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionInterfaceID = PermissionInterfaceID,
				RoleID = RoleID,
				OrderByPermissionInterfaceRole = OrderByPermissionInterfaceRole,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionInterfaceRole>>>(ApiCall.QueryString("/api/Authenticate/CX01701", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetPermissionInterfaceRoleAllSelector<T>(Guid? ID = default(Guid?), Guid? PermissionInterfaceID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<PermissionInterfaceRole, object>>, string> OrderByPermissionInterfaceRole = default(Dictionary<Expression<Func<PermissionInterfaceRole, object>>, string>), Expression<Func<PermissionInterfaceRole, T>> SelectorPermissionInterfaceRole = default(Expression<Func<PermissionInterfaceRole, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PermissionInterfaceID = PermissionInterfaceID,
				RoleID = RoleID,
				OrderByPermissionInterfaceRole = OrderByPermissionInterfaceRole,
				SelectorPermissionInterfaceRole = SelectorPermissionInterfaceRole,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01702", request));
		}

		/// <summary>
        /// 获取接口角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionInterfaceRole>> Authenticate_GetPermissionInterfaceRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionInterfaceRole>>(ApiCall.QueryString("/api/Authenticate/CX01703", request));
		}

		/// <summary>
        /// 添加/修改角色表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">角色表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<Role>> Authenticate_EditRole(bool IsAdd = default(bool), Role Model = default(Role))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<Role>>(ApiCall.QueryString("/api/Authenticate/XG01801", request), Model);
		}

		/// <summary>
        /// 删除角色表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01801", request), null);
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
		public static async Task<ApiResult<PagedList<Role>>> Authenticate_GetRoleAll(Guid? ID = default(Guid?), string Name = default(string), string Description = default(string), Dictionary<Expression<Func<Role, object>>, string> OrderByRole = default(Dictionary<Expression<Func<Role, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Description = Description,
				OrderByRole = OrderByRole,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Role>>>(ApiCall.QueryString("/api/Authenticate/CX01801", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetRoleAllSelector<T>(Guid? ID = default(Guid?), string Name = default(string), string Description = default(string), Dictionary<Expression<Func<Role, object>>, string> OrderByRole = default(Dictionary<Expression<Func<Role, object>>, string>), Expression<Func<Role, T>> SelectorRole = default(Expression<Func<Role, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Name = Name,
				Description = Description,
				OrderByRole = OrderByRole,
				SelectorRole = SelectorRole,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01802", request));
		}

		/// <summary>
        /// 获取角色表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<Role>> Authenticate_GetRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Role>>(ApiCall.QueryString("/api/Authenticate/CX01803", request));
		}

		/// <summary>
        /// 添加/修改用户表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<User>> Authenticate_EditUser(bool IsAdd = default(bool), User Model = default(User))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<User>>(ApiCall.QueryString("/api/Authenticate/XG01901", request), Model);
		}

		/// <summary>
        /// 删除用户表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteUser(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC01901", request), null);
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
		public static async Task<ApiResult<PagedList<User>>> Authenticate_GetUserAll(Guid? ID = default(Guid?), string Account = default(string), string Password = default(string), string Name = default(string), DateTime? CreateTime = default(DateTime?), byte[] Signature = default(byte[]), byte[] Fingerprint = default(byte[]), byte[] FingerprintSpare = default(byte[]), int? IsDeleted = default(int?), Dictionary<Expression<Func<User, object>>, string> OrderByUser = default(Dictionary<Expression<Func<User, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Account = Account,
				Password = Password,
				Name = Name,
				CreateTime = CreateTime,
				Signature = Signature,
				Fingerprint = Fingerprint,
				FingerprintSpare = FingerprintSpare,
				IsDeleted = IsDeleted,
				OrderByUser = OrderByUser,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<User>>>(ApiCall.QueryString("/api/Authenticate/CX01901", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetUserAllSelector<T>(Guid? ID = default(Guid?), string Account = default(string), string Password = default(string), string Name = default(string), DateTime? CreateTime = default(DateTime?), byte[] Signature = default(byte[]), byte[] Fingerprint = default(byte[]), byte[] FingerprintSpare = default(byte[]), int? IsDeleted = default(int?), Dictionary<Expression<Func<User, object>>, string> OrderByUser = default(Dictionary<Expression<Func<User, object>>, string>), Expression<Func<User, T>> SelectorUser = default(Expression<Func<User, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				Account = Account,
				Password = Password,
				Name = Name,
				CreateTime = CreateTime,
				Signature = Signature,
				Fingerprint = Fingerprint,
				FingerprintSpare = FingerprintSpare,
				IsDeleted = IsDeleted,
				OrderByUser = OrderByUser,
				SelectorUser = SelectorUser,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX01902", request));
		}

		/// <summary>
        /// 获取用户表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<User>> Authenticate_GetUser(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<User>>(ApiCall.QueryString("/api/Authenticate/CX01903", request));
		}

		/// <summary>
        /// 添加/修改用户登录令牌表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户登录令牌表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<UserToken>> Authenticate_EditUserToken(bool IsAdd = default(bool), UserToken Model = default(UserToken))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<UserToken>>(ApiCall.QueryString("/api/Authenticate/XG02001", request), Model);
		}

		/// <summary>
        /// 删除用户登录令牌表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteUserToken(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC02001", request), null);
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
		public static async Task<ApiResult<PagedList<UserToken>>> Authenticate_GetUserTokenAll(Guid? ID = default(Guid?), Guid? UserID = default(Guid?), Guid? ApplicationID = default(Guid?), string Token = default(string), DateTime? UpdateTime = default(DateTime?), Guid? UsingApplicationID = default(Guid?), Dictionary<Expression<Func<UserToken, object>>, string> OrderByUserToken = default(Dictionary<Expression<Func<UserToken, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				UserID = UserID,
				ApplicationID = ApplicationID,
				Token = Token,
				UpdateTime = UpdateTime,
				UsingApplicationID = UsingApplicationID,
				OrderByUserToken = OrderByUserToken,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<UserToken>>>(ApiCall.QueryString("/api/Authenticate/CX02001", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetUserTokenAllSelector<T>(Guid? ID = default(Guid?), Guid? UserID = default(Guid?), Guid? ApplicationID = default(Guid?), string Token = default(string), DateTime? UpdateTime = default(DateTime?), Guid? UsingApplicationID = default(Guid?), Dictionary<Expression<Func<UserToken, object>>, string> OrderByUserToken = default(Dictionary<Expression<Func<UserToken, object>>, string>), Expression<Func<UserToken, T>> SelectorUserToken = default(Expression<Func<UserToken, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				UserID = UserID,
				ApplicationID = ApplicationID,
				Token = Token,
				UpdateTime = UpdateTime,
				UsingApplicationID = UsingApplicationID,
				OrderByUserToken = OrderByUserToken,
				SelectorUserToken = SelectorUserToken,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX02002", request));
		}

		/// <summary>
        /// 获取用户登录令牌表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<UserToken>> Authenticate_GetUserToken(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<UserToken>>(ApiCall.QueryString("/api/Authenticate/CX02003", request));
		}

		/// <summary>
        /// 添加/修改用户角色关系表
        /// </summary>
        /// <param name="IsAdd">是否添加</param>
        /// <param name="Model">用户角色关系表实体</param>
        /// <returns>添加或修改的实体类</returns>
		public static async Task<ApiResult<UserToRole>> Authenticate_EditUserToRole(bool IsAdd = default(bool), UserToRole Model = default(UserToRole))
		{
			var request = new
			{
				IsAdd = IsAdd,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult<UserToRole>>(ApiCall.QueryString("/api/Authenticate/XG02101", request), Model);
		}

		/// <summary>
        /// 删除用户角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult> Authenticate_DeleteUserToRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Authenticate/SC02101", request), null);
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
		public static async Task<ApiResult<PagedList<UserToRole>>> Authenticate_GetUserToRoleAll(Guid? ID = default(Guid?), Guid? UserID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<UserToRole, object>>, string> OrderByUserToRole = default(Dictionary<Expression<Func<UserToRole, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				UserID = UserID,
				RoleID = RoleID,
				OrderByUserToRole = OrderByUserToRole,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<UserToRole>>>(ApiCall.QueryString("/api/Authenticate/CX02101", request));
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
		public static async Task<ApiResult<PagedList<T>>> Authenticate_GetUserToRoleAllSelector<T>(Guid? ID = default(Guid?), Guid? UserID = default(Guid?), Guid? RoleID = default(Guid?), Dictionary<Expression<Func<UserToRole, object>>, string> OrderByUserToRole = default(Dictionary<Expression<Func<UserToRole, object>>, string>), Expression<Func<UserToRole, T>> SelectorUserToRole = default(Expression<Func<UserToRole, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				UserID = UserID,
				RoleID = RoleID,
				OrderByUserToRole = OrderByUserToRole,
				SelectorUserToRole = SelectorUserToRole,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Authenticate/CX02102", request));
		}

		/// <summary>
        /// 获取用户角色关系表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<UserToRole>> Authenticate_GetUserToRole(Guid ID = default(Guid))
		{
			var request = new
			{
				ID = ID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<UserToRole>>(ApiCall.QueryString("/api/Authenticate/CX02103", request));
		}

		/// <summary>
        /// 添加/修改组织架构
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditOrganization(Organization model = default(Organization))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00401", request), model);
		}

		/// <summary>
        /// 删除组织架构
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelOrganization(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00401", request), null);
		}

		/// <summary>
        /// 获取组织架构
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<Organization>> Permission_GetOrganization(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Organization>>(ApiCall.QueryString("/api/Permission/CX00401", request));
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
		public static async Task<ApiResult<PagedList<Organization>>> Permission_GetOrganizationAll(List<Expression<Func<Organization, bool>>> PredicateOrganization = default(List<Expression<Func<Organization, bool>>>), Dictionary<Expression<Func<Organization, object>>, string> OrderByOrganization = default(Dictionary<Expression<Func<Organization, object>>, string>), string Includes = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PredicateOrganization = PredicateOrganization,
				OrderByOrganization = OrderByOrganization,
				Includes = Includes,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Organization>>>(ApiCall.QueryString("/api/Permission/CX00402", request));
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
		public static async Task<ApiResult<PagedList<T>>> Permission_GetOrganizationAllSelector<T>(List<Expression<Func<Organization, bool>>> PredicateOrganization = default(List<Expression<Func<Organization, bool>>>), Dictionary<Expression<Func<Organization, object>>, string> OrderByOrganization = default(Dictionary<Expression<Func<Organization, object>>, string>), Expression<Func<Organization, T>> SelectorOrganization = default(Expression<Func<Organization, T>>), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				PredicateOrganization = PredicateOrganization,
				OrderByOrganization = OrderByOrganization,
				SelectorOrganization = SelectorOrganization,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<T>>>(ApiCall.QueryString("/api/Permission/CX00403", request));
		}

		/// <summary>
        /// 获得全部组织架构
        /// </summary>
        /// <returns></returns>
		public static async Task<ApiResult<List<Organization>>> Permission_GetOrganizationList()
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<Organization>>>(ApiCall.QueryString("/api/Permission/CX00405", request));
		}

		/// <summary>
        /// 根据父级ID获得组织架构列表
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<List<Organization>>> Permission_GetOrganizationListByPID(Guid? PID = default(Guid?))
		{
			var request = new
			{
				PID = PID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<Organization>>>(ApiCall.QueryString("/api/Permission/CX00406", request));
		}

		/// <summary>
        /// 获取通用组织下通用名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// [ActionName("")]
		public static async Task<ApiResult<List<OrganizationGeneral>>> Permission_GetOrganizationGeneralByType(string type = default(string))
		{
			var request = new
			{
				type = type,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<OrganizationGeneral>>>(ApiCall.QueryString("/api/Permission/GetOrganizationGeneralByType", request));
		}

		
		public static async Task<ApiResult> Permission_EditOrganizationGeneral(OrganizationGeneral model = default(OrganizationGeneral))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/EditOrganizationGeneral", request), model);
		}

		
		public static async Task<ApiResult<OrganizationGeneral>> Permission_GetOrganizationGeneral(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<OrganizationGeneral>>(ApiCall.QueryString("/api/Permission/GetOrganizationGeneral", request));
		}

		
		public static async Task<ApiResult> Permission_DelOrganizationGeneral(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/DelOrganizationGeneral", request), null);
		}

		
		public static async Task<ApiResult<PagedList<OrganizationGeneral>>> Permission_GetOrganizationGeneralAll(string Type = default(string), string Name = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Type = Type,
				Name = Name,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<OrganizationGeneral>>>(ApiCall.QueryString("/api/Permission/GetOrganizationGeneralAll", request));
		}

		/// <summary>
        /// 添加/修改应用
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditApplication(Application model = default(Application))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00501", request), model);
		}

		/// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelApplication(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00501", request), null);
		}

		/// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<Application>> Permission_GetApplication(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Application>>(ApiCall.QueryString("/api/Permission/CX00501", request));
		}

		/// <summary>
        /// 获得全部应用
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Space">命名空间</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<Application>>> Permission_GetApplicationAll(string Name = default(string), string Space = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				Space = Space,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Application>>>(ApiCall.QueryString("/api/Permission/CX00502", request));
		}

		/// <summary>
        /// 获得全部应用
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
		public static async Task<ApiResult<List<Application>>> Permission_GetApplicationAllOrderByRolePermissionBtn(Guid? RoleID = default(Guid?))
		{
			var request = new
			{
				RoleID = RoleID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<Application>>>(ApiCall.QueryString("/api/Permission/CX00503", request));
		}

		/// <summary>
        /// 添加/修改窗体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditPermissionGroup(PermissionGroup model = default(PermissionGroup))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00601", request), model);
		}

		/// <summary>
        /// 删除窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelPermissionGroup(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00601", request), null);
		}

		/// <summary>
        /// 获取窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionGroup>> Permission_GetPermissionGroup(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionGroup>>(ApiCall.QueryString("/api/Permission/CX00601", request));
		}

		/// <summary>
        /// 获取窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<List<PermissionGroup>>> Permission_GetPermissionGroupByPID(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<PermissionGroup>>>(ApiCall.QueryString("/api/Permission/CX00605", request));
		}

		/// <summary>
        /// 根据名称获取窗体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionGroup>> Permission_GetPermissionGroupName(string name = default(string))
		{
			var request = new
			{
				name = name,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionGroup>>(ApiCall.QueryString("/api/Permission/CX00604", request));
		}

		/// <summary>
        /// 获得全部窗体
        /// </summary>
        /// <param name="ApplicationID"></param>
        /// <returns></returns>
		public static async Task<ApiResult<List<PermissionGroup>>> Permission_GetPermissionGroupList(Guid? ApplicationID = default(Guid?))
		{
			var request = new
			{
				ApplicationID = ApplicationID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<PermissionGroup>>>(ApiCall.QueryString("/api/Permission/CX00603", request));
		}

		/// <summary>
        /// 获得全部窗体
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="AppName">应用名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<sp_PermissionGroupQuery_Result>>> Permission_GetPermissionGroupAll(string Name = default(string), string AppName = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				AppName = AppName,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<sp_PermissionGroupQuery_Result>>>(ApiCall.QueryString("/api/Permission/CX00602", request));
		}

		/// <summary>
        /// 添加/修改窗体对应的按钮
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditPermissionBtn(PermissionBtn model = default(PermissionBtn))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00701", request), model);
		}

		/// <summary>
        /// 删除窗体对应的按钮
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelPermissionBtn(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00701", request), null);
		}

		/// <summary>
        /// 获取窗体对应的按钮
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionBtn>> Permission_GetPermissionBtn(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionBtn>>(ApiCall.QueryString("/api/Permission/CX00701", request));
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
		public static async Task<ApiResult<PagedList<V_PermissionBtnQuery>>> Permission_GetPermissionBtnAll(string ButtonName = default(string), string FormName = default(string), string DisplayName = default(string), Guid? ApplicationID = default(Guid?), Guid? PermissionGroupID = default(Guid?), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ButtonName = ButtonName,
				FormName = FormName,
				DisplayName = DisplayName,
				ApplicationID = ApplicationID,
				PermissionGroupID = PermissionGroupID,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<V_PermissionBtnQuery>>>(ApiCall.QueryString("/api/Permission/CX00702", request));
		}

		/// <summary>
        /// 添加/修改权限接口
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditPermissionInterface(PermissionInterface model = default(PermissionInterface))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00703", request), model);
		}

		/// <summary>
        /// 删除权限接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelPermissionInterface(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00702", request), null);
		}

		/// <summary>
        /// 获取权限接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionInterface>> Permission_GetPermissionInterface(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionInterface>>(ApiCall.QueryString("/api/Permission/CX00703", request));
		}

		/// <summary>
        /// 获得全部权限接口
        /// </summary>
        /// <param name="InterfaceName">接口名</param>
        /// <param name="ApplicationID">应用ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<PermissionInterface>>> Permission_GetPermissionInterfaceAll(string InterfaceName = default(string), Guid? ApplicationID = default(Guid?), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				InterfaceName = InterfaceName,
				ApplicationID = ApplicationID,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<PermissionInterface>>>(ApiCall.QueryString("/api/Permission/CX00704", request));
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
		public static async Task<ApiResult<PagedList<V_PermissionInterfaceQuery>>> Permission_GetPermissionInterfaceViewAll(string Name = default(string), string MethodName = default(string), Guid? ApplicationID = default(Guid?), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				MethodName = MethodName,
				ApplicationID = ApplicationID,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<V_PermissionInterfaceQuery>>>(ApiCall.QueryString("/api/Permission/CX00705", request));
		}

		/// <summary>
        /// 添加/修改用户角色关系
        /// </summary>
        /// <param name="lst">用户角色关系列表</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditUserToRole(List<UserToRole> lst = default(List<UserToRole>))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00801", request), lst);
		}

		/// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelUserToRole(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00801", request), null);
		}

		/// <summary>
        /// 获取用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<UserToRole>> Permission_GetUserToRole(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<UserToRole>>(ApiCall.QueryString("/api/Permission/CX00801", request));
		}

		/// <summary>
        /// 获得全部用户角色关系
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<UserToRole>>> Permission_GetUserToRoleAll(string Name = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<UserToRole>>>(ApiCall.QueryString("/api/Permission/CX00802", request));
		}

		/// <summary>
        /// 添加/修改用户角色关系
        /// </summary>
        /// <param Name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_EditPermissionBtnRole(Guid RoleID = default(Guid), List<PermissionBtnRole> List = default(List<PermissionBtnRole>))
		{
			var request = new
			{
				RoleID = RoleID,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/XG00901", request), List);
		}

		/// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns></returns>
		public static async Task<ApiResult> Permission_DelPermissionBtnRole(List<PermissionBtnRole> list = default(List<PermissionBtnRole>))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Permission/SC00901", request), list);
		}

		/// <summary>
        /// 获取用户角色关系
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<PermissionBtnRole>> Permission_GetPermissionBtnRole(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PermissionBtnRole>>(ApiCall.QueryString("/api/Permission/CX00901", request));
		}

		/// <returns></returns>
		public static async Task<ApiResult<List<PermissionBtnRole>>> Permission_GetPermissionBtnRoleAll(Guid RoleID = default(Guid))
		{
			var request = new
			{
				RoleID = RoleID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<PermissionBtnRole>>>(ApiCall.QueryString("/api/Permission/CX00902", request));
		}

		/// <summary>
        /// 添加/修改消息返回码
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Public_EditCommonWords(CommonWords model = default(CommonWords))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Public/XG01001", request), model);
		}

		/// <summary>
        /// 删除消息返回码
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Public_DelCommonWords(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Public/SC01001", request), null);
		}

		/// <summary>
        /// 获取消息返回码
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<CommonWords>> Public_GetCommonWords(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<CommonWords>>(ApiCall.QueryString("/api/Public/CX01001", request));
		}

		/// <summary>
        /// 获得全部消息返回码
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<CommonWords>>> Public_GetCommonWordsAll(string Name = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<CommonWords>>>(ApiCall.QueryString("/api/Public/CX01002", request));
		}

		/// <summary>
        /// 添加/修改接口
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> Public_EditInterface(InterfaceMain model = default(InterfaceMain))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Public/XG01301", request), model);
		}

		/// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> Public_DelInterface(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/Public/SC01301", request), null);
		}

		/// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceMain>> Public_GetInterface(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceMain>>(ApiCall.QueryString("/api/Public/CX01301", request));
		}

		/// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<InterfaceDetail>> Public_GetInterfaceDetail(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<InterfaceDetail>>(ApiCall.QueryString("/api/Public/CX01303", request));
		}

		/// <summary>
        /// 获得全部接口
        /// </summary>
        /// <param name="InterfaceNo">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<InterfaceMain>>> Public_GetInterfaceAll(string InterfaceNo = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				InterfaceNo = InterfaceNo,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceMain>>>(ApiCall.QueryString("/api/Public/CX01302", request));
		}

		/// <summary>
        /// 获得全部接口历史
        /// </summary>
        /// <param name="ID">接口ID</param>
        /// <param name="PageIndex">第几页</param>uiu
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<InterfaceMainHist>>> Public_GetInterfaceHisAll(Guid ID = default(Guid), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceMainHist>>>(ApiCall.QueryString("/api/Public/CX01304", request));
		}

		/// <summary>
        /// 获得全部接口历史
        /// </summary>
        /// <param name="ID">接口ID</param>
        /// <param name="PageIndex">第几页</param>uiu
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<InterfaceDetailHist>>> Public_GetInterfaceDetaiHisAll(Guid ID = default(Guid), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				ID = ID,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<InterfaceDetailHist>>>(ApiCall.QueryString("/api/Public/CX01305", request));
		}

		/// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="Id">用户编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<User>> User_GetUserInfo(Guid Id = default(Guid))
		{
			var request = new
			{
				Id = Id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<User>>(ApiCall.QueryString("/api/User/CX00201", request));
		}

		/// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Name">姓名</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<User>>> User_GetUserInfoAll(string Account = default(string), string Name = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Account = Account,
				Name = Name,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<User>>>(ApiCall.QueryString("/api/User/CX00202", request));
		}

		/// <summary>
        /// 根据组织架构编号获得用户
        /// </summary>
        /// <param name="OrgID">组织架构编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<List<User>>> User_GetUserByOrgID(Guid OrgID = default(Guid))
		{
			var request = new
			{
				OrgID = OrgID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<List<User>>>(ApiCall.QueryString("/api/User/CX00203", request));
		}

		/// <summary>
        /// 根据门诊编号获得用户
        /// </summary>
        /// <param name="CID">门诊编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<IQueryable<V_UserToRoleQuery>>> User_GetUserByCID(string CID = default(string))
		{
			var request = new
			{
				CID = CID,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<IQueryable<V_UserToRoleQuery>>>(ApiCall.QueryString("/api/User/CX00204", request));
		}

		/// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="Account">用户账号</param>
        /// <returns></returns>
		public static async Task<ApiResult<User>> User_GetUserInfo(string Account = default(string))
		{
			var request = new
			{
				Account = Account,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<User>>(ApiCall.QueryString("/api/User/CX00206", request));
		}

		/// <summary>
        /// 添加/修改用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> User_EditUserInfo(User model = default(User))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/User/XG00201", request), model);
		}

		/// <summary>
        /// 启用停用
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="tag">状态（0：启用；1：停用）</param>
        /// <returns></returns>
		public static async Task<ApiResult> User_DelUserInfo(Guid id = default(Guid), int tag = default(int))
		{
			var request = new
			{
				id = id,
				tag = tag,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/User/SC00201", request), null);
		}

		/// <summary>
        /// 根据用户编号+具体系统获取对应功能权限
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="Application">应用名称</param>
        /// <returns>用户</returns>
		public static async Task<ApiResult<IQueryable<V_UserPermissionQuery>>> User_GetUserPermission(string UserID = default(string), string Application = default(string))
		{
			var request = new
			{
				UserID = UserID,
				Application = Application,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<IQueryable<V_UserPermissionQuery>>>(ApiCall.QueryString("/api/User/CX00205", request));
		}

		/// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> User_AddRole(Role model = default(Role))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/User/XZ00301", request), model);
		}

		/// <summary>
        /// 添加/修改角色
        /// </summary>
        /// <param name="model">角色实体</param>
        /// <returns></returns>
		public static async Task<ApiResult> User_EditRole(Role model = default(Role))
		{
			var request = new
			{
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/User/XG00301", request), model);
		}

		/// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
		public static async Task<ApiResult> User_DelRole(Guid id = default(Guid))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.PostAsync<ApiResult>(ApiCall.QueryString("/api/User/SC00301", request), null);
		}

		/// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
		public static async Task<ApiResult<Role>> User_GetRole(Guid? id = default(Guid?))
		{
			var request = new
			{
				id = id,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<Role>>(ApiCall.QueryString("/api/User/CX00301", request));
		}

		/// <summary>
        /// 获得全部角色
        /// </summary>
        /// <param name="Name">角色名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<Role>>> User_GetRoleAll(string Name = default(string), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				Name = Name,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<Role>>>(ApiCall.QueryString("/api/User/CX00302", request));
		}

		
		public static async Task<ApiResult<PagedList<V_UserToRoleQuery>>> User_GetRoleToUser(Guid id = default(Guid), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				id = id,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<V_UserToRoleQuery>>>(ApiCall.QueryString("/api/User/GetRoleToUser", request));
		}

		/// <summary>
        /// 根据用户查询用户有多少个角色
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
		public static async Task<ApiResult<PagedList<V_UserToRoleQuery>>> User_GetUserToRole(Guid id = default(Guid), int PageIndex = default(int), int PageSize = default(int))
		{
			var request = new
			{
				id = id,
				PageIndex = PageIndex,
				PageSize = PageSize,
			};
			return await ApiEnssiAuthenticate.GetAsync<ApiResult<PagedList<V_UserToRoleQuery>>>(ApiCall.QueryString("/api/User/CX00304", request));
		}
	}
}

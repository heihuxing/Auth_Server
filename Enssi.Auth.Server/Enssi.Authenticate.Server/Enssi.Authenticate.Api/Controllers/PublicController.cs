using Enssi.Authenticate.Data;
using Enssi.Authenticate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Enssi.Authenticate.Api.Controllers
{
    public class PublicController : ApiController
    {
        private EnssiAuthenticateEntities db => Api.DbEnssiAuthenticate;

        #region 消息返回码
        /// <summary>
        /// 添加/修改消息返回码
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG01001")]
        [HttpPost]
        public ApiResult EditCommonWords(CommonWords model)
        {
            var ID = model.ID;
            if (model.ID == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                db.CommonWords.Add(model);
            }
            else
            {
                db.CommonWords.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除消息返回码
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC01001")]
        [HttpPost]
        public ApiResult DelCommonWords(Guid id)
        {
            var CommonWords = new CommonWords() { ID = id };
            db.CommonWords.Attach(CommonWords);
            db.CommonWords.Remove(CommonWords);
            db.SaveChanges();
            return new ApiResult { Message = "删除成功！" };
        }

        /// <summary>
        /// 获取消息返回码
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX01001")]
        [HttpGet]
        public ApiResult<CommonWords> GetCommonWords(Guid? id)
        {
            CommonWords obj =  db.CommonWords.FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<CommonWords> { Data = obj };
        }

        /// <summary>
        /// 获得全部消息返回码
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01002")]
        [HttpGet]
        public ApiResult<PagedList<CommonWords>> GetCommonWordsAll(string Name, int PageIndex, int PageSize)
        {
            string Str = "";
            if (Name != null)
            {
                Str = Name;
            }
            IQueryable<CommonWords> List = db.CommonWords.Where(i => i.CommonName.Contains(Str));
            return new ApiResult<PagedList<CommonWords>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }

        #endregion

        #region 下拉框参数

        ///// <summary>
        ///// 添加/修改下拉框参数
        ///// </summary>
        ///// <param name="model">实体</param>
        ///// <returns>XG01201</returns>
        //[System.Web.Http.HttpPost]
        //public ApiResult EditParameter(Parameter model)
        //{
        //    var id = model.ID;
        //    return new ApiResult { Message = (id != 0 ? "修改" : "添加") + "成功！" };
        //}

        ///// <summary>
        ///// 删除下拉框参数
        ///// </summary>
        ///// <param name="id">编号</param>
        ///// <returns>SC01201</returns>
        //[HttpPost]
        //public ApiResult DelParameter(int id)
        //{

        //    return new ApiResult { Message = "删除成功！" };
        //}

        ///// <summary>
        ///// 获取下拉框参数
        ///// </summary>
        ///// <param name="id">编号</param>
        ///// <returns>CX01201</returns>
        //[HttpGet]
        //public ApiResult<Parameter> GetParameter(int? id)
        //{

        //    return new ApiResult<Parameter> { Data = new Parameter() };
        //}

        ///// <summary>
        ///// 获得全部下拉框参数
        ///// </summary>
        ///// <param name="Name">名称</param>
        ///// <param name="PageIndex">第几页</param>
        ///// <param name="PageSize">页大小</param>
        ///// <returns>CX01202</returns>
        //[HttpGet]
        //public ApiResult<PagedList<Parameter>> GetParameterAll(int ParameterTypeID)
        //{

        //    return new ApiResult<PagedList<Parameter>> { Data = null };
        //}

        #endregion

        #region 集团化参数

        #endregion

        #region 接口
        /// <summary>
        /// 添加/修改接口
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [ActionName("XG01301")]
        [HttpPost]
        public ApiResult EditInterface(InterfaceMain model)
        {
            var ID = model.ID;
            if (model.ID == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                foreach (InterfaceDetail obj in model.InterfaceDetail)
                {
                    obj.InterfaceID = model.ID;
                }
                db.InterfaceMain.Add(model);
                db.SaveChanges();
            }
            else
            {
                List<InterfaceDetail> listdetail = model.InterfaceDetail.ToList();
                db.InterfaceMain.Attach(model);
                var Entry = db.Entry(model);
                Entry.State = System.Data.Entity.EntityState.Modified;
                IQueryable<InterfaceDetail> list = db.InterfaceDetail.Where(i=>i.InterfaceID==model.ID);          
                foreach (InterfaceDetail obj in list)
                {
                    db.InterfaceDetail.Remove(obj);
                }
                db.SaveChanges();
                foreach (InterfaceDetail obj in listdetail)
                {
                    obj.InterfaceID = model.ID;
                    db.InterfaceDetail.Add(obj);
                }
                db.SaveChanges();
            }
     
            return new ApiResult { Message = (ID != Guid.Empty ? "修改" : "添加") + "成功！" };
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("SC01301")]
        [HttpPost]
        public ApiResult DelInterface(Guid id)
        {
            InterfaceMain Obj = db.InterfaceMain.Where(i => i.ID == id).FirstOrDefault();
            if (Obj != null)
            {
                Obj.IsDeleted = 1;
                db.InterfaceMain.Attach(Obj);
                var Entry = db.Entry(Obj);
                Entry.State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return new ApiResult { Message = "删除成功！" };
            }
            else
            {
                return new ApiResult { ErrorCode = 1, Message = "删除失败！" };
            }
        }

        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX01301")]
        [HttpGet]
        public ApiResult<InterfaceMain> GetInterface(Guid? id)
        {
            InterfaceMain obj = db.InterfaceMain.Include("InterfaceDetail").FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<InterfaceMain> { Data = obj };
        }

        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [ActionName("CX01303")]
        [HttpGet]
        public ApiResult<InterfaceDetail> GetInterfaceDetail(Guid? id)
        {
            InterfaceDetail obj = db.InterfaceDetail.FirstOrDefault(i => i.ID == id.Value);
            return new ApiResult<InterfaceDetail> { Data = obj };
        }

        /// <summary>
        /// 获得全部接口
        /// </summary>
        /// <param name="InterfaceNo">名称</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01302")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceMain>> GetInterfaceAll(string InterfaceNo, int PageIndex, int PageSize)
        {
            string Str = "";
            if (InterfaceNo != null)
            {
                Str = InterfaceNo;
            }
            IQueryable<InterfaceMain> List = db.InterfaceMain.Where(i => i.InterfaceNo.Contains(Str) && i.IsDeleted == 0).OrderBy(i=>i.CreateDate);
            return new ApiResult<PagedList<InterfaceMain>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获得全部接口历史
        /// </summary>
        /// <param name="ID">接口ID</param>
        /// <param name="PageIndex">第几页</param>uiu
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01304")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceMainHist>> GetInterfaceHisAll(Guid ID, int PageIndex, int PageSize)
        {
            IQueryable<InterfaceMainHist> List = db.InterfaceMainHist.Where(i => i.ID==ID && i.IsDeleted == 0).OrderBy(i => i.CreateDate);
            return new ApiResult<PagedList<InterfaceMainHist>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }

        /// <summary>
        /// 获得全部接口历史
        /// </summary>
        /// <param name="ID">接口ID</param>
        /// <param name="PageIndex">第几页</param>uiu
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        [ActionName("CX01305")]
        [HttpGet]
        public ApiResult<PagedList<InterfaceDetailHist>> GetInterfaceDetaiHisAll(Guid ID, int PageIndex, int PageSize)
        {
            IQueryable<InterfaceDetailHist> List = db.InterfaceDetailHist.Where(i => i.ID == ID).OrderBy(i => i.ParameterNo);
            return new ApiResult<PagedList<InterfaceDetailHist>> { Data = List.ToPagedList(PageIndex, PageSize) };
        }
        #endregion
    }
}

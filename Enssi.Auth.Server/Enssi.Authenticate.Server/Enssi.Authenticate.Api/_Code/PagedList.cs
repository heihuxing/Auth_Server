using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enssi
{
    public class PagedList<T>
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int RecordCount { get; set; }
        public List<T> List { get; set; }
    }

    public static class PagedListExpand
    {
        /// <summary>
        /// 查询分页集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">待分页语句</param>
        /// <param name="pageIndex">当前页（0为查询全部，-1为查询前多少条）</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            List<T> list = null;

            if (pageIndex > 0)
            {
                list = queryable.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
            }
            else if (pageIndex == 0)
            {
                list = queryable.ToList();
            }
            else if (pageIndex == -1)
            {
                list = queryable.Take(pageSize).ToList();
            }

            var pagedlist = new PagedList<T>();
            pagedlist.RecordCount = pageIndex == 0 ? list.Count : pageIndex == -1 ? pageSize : queryable.Count();
            pagedlist.PageSize = pageSize;
            pagedlist.PageIndex = pageIndex;
            pagedlist.PageCount = (int)Math.Ceiling(pagedlist.RecordCount / (double)pageSize);
            pagedlist.List = list;

            return pagedlist;
        }

        public static PagedList<TResult> CastPagedList<TResult>(this PagedList<object> source)
        {
            var paged = new PagedList<TResult>();
            paged.List = source.List.Select(a => (TResult)a).ToList();
            paged.PageCount = source.PageCount;
            paged.PageIndex = source.PageIndex;
            paged.PageSize = source.PageSize;
            paged.RecordCount = source.RecordCount;
            return paged;
        }
    }
}
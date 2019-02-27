using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enssi
{
    /// <summary>
    /// Api返回类型
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Api返回类型带数据
    /// </summary>
    public class ApiResult<T>
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}

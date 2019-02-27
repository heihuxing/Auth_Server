using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Enssi.Authenticate.Api
{
    /// <summary>
    /// 强制GZip压缩，application/json
    /// Content-encoding:gzip
    /// GZIP是使用DEFLATE进行压缩数据的另一个压缩库
    /// 强制Defalte压缩
    /// Content-encoding:deflate
    /// DEFLATE是一个无专利的压缩算法，它可以实现无损数据压缩，有众多开源的实现算法。
    /// </summary>
    public class CompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            HttpResponse response = HttpContext.Current.Response;

            switch (filterContext.Request.Headers.AcceptEncoding.FirstOrDefault()?.Value)
            {
                case "gzip":
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.Headers["Content-Encoding"] = "gzip";
                    break;
                case "deflate":
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    response.Headers["Content-Encoding"] = "deflate";
                    break;
                default:
                    break;
            }
        }
    }
}
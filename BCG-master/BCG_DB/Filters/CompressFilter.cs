namespace BCG_DB.Filters
{
    using System.IO.Compression;
    using System.Web;
    using System.Web.Mvc;

    public class CompressFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (acceptEncoding.Contains("GZIP"))
            {

                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress, true);
                response.AppendHeader("Content-encoding", "gzip");
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {

                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress, true);
                response.AppendHeader("Content-encoding", "deflate");
            }
        }
    }
}

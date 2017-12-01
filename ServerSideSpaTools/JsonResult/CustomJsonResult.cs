using jsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Base json result, with typing.
    /// </summary>
    public abstract class CustomJsonResult : jsonResult
    {
        public CustomJsonResult(object value) : base(value) { }
    }
}

using jsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace WebAppUtilities.JsonResult
{
    public class CustomJsonResult : jsonResult
    {
        public CustomJsonResult(object value) : base(value) { }
    }
}

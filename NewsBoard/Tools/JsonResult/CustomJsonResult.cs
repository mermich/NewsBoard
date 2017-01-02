using NewsBoard.Tools;
using jsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace NewsBoard.Tools.JsonResult
{
    public class CustomJsonResult : jsonResult
    {
        public CustomJsonResult(object value) : base(value) { }
    }
}

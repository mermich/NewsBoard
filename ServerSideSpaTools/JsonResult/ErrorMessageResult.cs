
namespace ServerSideSpaTools.JsonResult
{
    public class ErrorMessageResult : CustomJsonResult
    {
        public ErrorMessageResult(string message) : base(new { ErrorMessage = message })
        {

        }
    }
}

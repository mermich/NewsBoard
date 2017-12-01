
namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Displays an error message.
    /// </summary>
    public class ErrorMessageResult : CustomJsonResult
    {
        public ErrorMessageResult(string message) : base(new { ErrorMessage = message })
        {

        }
    }
}

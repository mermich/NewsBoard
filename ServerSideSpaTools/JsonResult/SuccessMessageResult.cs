namespace ServerSideSpaTools.JsonResult
{
    public class SuccessMessageResult : CustomJsonResult
    {
        public SuccessMessageResult(string message) : base(new { SuccessMessage = message })
        {

        }
    }
}

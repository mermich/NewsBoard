namespace ServerSideSpaTools.JsonResult
{
    public class CallbackResult : CustomJsonResult
    {
        public CallbackResult(CustomJsonResult firstAction, CustomJsonResult callback) : base(new { firstAction = firstAction, callback = callback })
        {
        }
    }
}

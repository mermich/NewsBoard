namespace ServerSideSpaTools.JsonResult
{
    public class SetTimeoutResult : CustomJsonResult
    {
        public SetTimeoutResult(int timeout, CustomJsonResult callback) : base(new { timeout = timeout, callback = callback.Value })
        {
        }
    }
}

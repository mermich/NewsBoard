namespace ServerSideSpaTools.JsonResult
{
    public class SetTimeoutResult : CustomJsonResult
    {
        public SetTimeoutResult(int timeout, CustomJsonResult callback) : base(new {  timeout, callback = callback.Value })
        {
        }
    }
}

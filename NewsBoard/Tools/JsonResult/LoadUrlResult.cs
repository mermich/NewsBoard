namespace NewsBoard.Tools.JsonResult
{
    public class LoadUrlResult : CustomJsonResult
    {
        public LoadUrlResult(string url) : base(new { loadUrl = url })
        {

        }
    }
}

namespace WebAppUtilities.JsonResult
{
    public class LoadUrlResult : CustomJsonResult
    {
        public LoadUrlResult(string url) : base(new { loadUrl = url })
        {

        }
    }
}

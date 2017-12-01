namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Reloads the entire page for a given url.
    /// </summary>
    public class LoadUrlResult : CustomJsonResult
    {
        public LoadUrlResult(string url) : base(new { loadUrl = url })
        {

        }
    }
}

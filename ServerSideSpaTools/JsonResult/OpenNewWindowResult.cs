namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Opens a new window for an url.
    /// </summary>
    public class OpenNewWindowResult : CustomJsonResult
    {
        public OpenNewWindowResult(string url) : base(new { OpenNewWindow = new {  url} })
        {

        }
    }
}

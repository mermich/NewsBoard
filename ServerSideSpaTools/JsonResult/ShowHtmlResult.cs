namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Show an element for a given css selector.
    /// </summary>
    public class ShowHtmlResult : CustomJsonResult
    {
        public ShowHtmlResult(string selector) : base(new { ShowHtml = new { selector = selector } })
        {

        }
    }
}

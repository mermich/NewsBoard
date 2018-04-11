namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Hides html for given css selector.
    /// </summary>
    public class HideHtmlResult : CustomJsonResult
    {
        public HideHtmlResult(string selector) : base(new { HideHtml = new {  selector } })
        {

        }
    }
}

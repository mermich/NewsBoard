namespace ServerSideSpaTools.JsonResult
{
    public class HideHtmlResult : CustomJsonResult
    {
        public HideHtmlResult(string selector) : base(new { HideHtml = new { selector = selector } })
        {

        }
    }
}

namespace ServerSideSpaTools.JsonResult
{
    public class RemoveHtmlResult : CustomJsonResult
    {
        public RemoveHtmlResult(string selector) : base(new { RemoveHtml = new { selector = selector } })
        {

        }
    }
}

namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Remove element for a css selector.
    /// </summary>
    public class RemoveHtmlResult : CustomJsonResult
    {
        public RemoveHtmlResult(string selector) : base(new { RemoveHtml = new {  selector } })
        {

        }
    }
}

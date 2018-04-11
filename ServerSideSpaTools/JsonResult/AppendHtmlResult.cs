namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Appends the result of an action to a css selector
    /// </summary>
    public class AppendHtmlResult : CustomJsonResult
    {
        public AppendHtmlResult(string selector, string action) : base(new { AppendHtml = new {  selector,  action } })
        {

        }
    }
}

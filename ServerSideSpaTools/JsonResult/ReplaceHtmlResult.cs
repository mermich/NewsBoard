namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Replaces an element with the result of an action for a given css selector.
    /// </summary>
    public class ReplaceHtmlResult : CustomJsonResult
    {
        public ReplaceHtmlResult(string selector, string action) : base(new { ReplaceHtml = new { selector = selector, action = action } })
        {

        }
    }
}

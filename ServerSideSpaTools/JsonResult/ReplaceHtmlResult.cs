namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Replaces an element with the result of an action for a given css selector.
    /// </summary>
    public class ReplaceHtmlResult : CustomJsonResult
    {
        public string Action { get; set; }


        public ReplaceHtmlResult(string selector, string action) : base(new { ReplaceHtml = new { selector, action } })
        {
            Action = action;
        }
    }
}

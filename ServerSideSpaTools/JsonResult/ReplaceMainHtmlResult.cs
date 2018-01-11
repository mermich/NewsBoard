using Microsoft.AspNetCore.Mvc;

namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Replaces main content with the result of an action.
    /// </summary>
    public class ReplaceMainHtmlResult : ReplaceHtmlResult
    {
        public ReplaceMainHtmlResult(string action) : base("#page", action)
        {

        }
    }
}

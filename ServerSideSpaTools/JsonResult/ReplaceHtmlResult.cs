using Microsoft.AspNetCore.Mvc;

namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Replaces an element with the result of an action for a given css selector.
    /// </summary>
    public class ReplaceHtmlResult : CustomJsonResult
    {
        public string Action { get; set; }

        public ReplaceHtmlResult(string selector, string action, string loaderClass = "") : base(new { ReplaceHtml = new { selector, action, loaderClass } })
        {
            Action = action;
        }

        /// <summary>
        /// Returns a ReplaceMainHtmlResult in ajax or swap to a RedirectResult if non ajax.
        /// </summary>
        /// <param name="res"></param>
        /// <param name="isAjaxRequest"></param>
        /// <returns></returns>
        public ActionResult ReplaceResultOrRedirectResult(bool isAjaxRequest)
        {
            if (isAjaxRequest)
            {
                return this;
            }
            else
            {
                return new RedirectResult(Action);
            }
        }
    }
}

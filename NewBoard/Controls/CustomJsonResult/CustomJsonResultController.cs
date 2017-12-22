using System;
using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Controls.CustomJsonResult
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>        
    [Area("Controls")]
    public partial class CustomJsonResultController : BaseController
    {
        public virtual IActionResult Index()
        {
            return ReturnView("CustomJsonResultView", new CustomJsonResultModel());
        }


        public virtual IActionResult SucessMessage()
        {
            return new SuccessMessageResult("Its superb sucess");
        }

        public virtual IActionResult ErrorMessage()
        {
            return new ErrorMessageResult("Its superb error");
        }

        public virtual IActionResult WarnMessage()
        {
            return new WarnMessageResult("Its superb warn");
        }

        public virtual IActionResult Compose()
        {
            return new ComposeResult(new WarnMessageResult("Its superb Warn"), new ErrorMessageResult("Its superb error"));
        }

        public virtual IActionResult ReplaceHtml()
        {
            return new ReplaceHtmlResult("#replaceDiv", Url.Action("GetTime"));
        }

        public virtual IActionResult AppendHtml()
        {
            return new AppendHtmlResult("#appendDiv", Url.Action("GetTime"));
        }

        public virtual IActionResult OpenNewWindow()
        {
            return new OpenNewWindowResult("http://www.google.com");
        }

        public virtual ActionResult GetTime()
        {
            return ReturnView("TimeView", DateTime.Now.Second);
        }
    }
}
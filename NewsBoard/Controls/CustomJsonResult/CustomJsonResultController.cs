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
    public class CustomJsonResultController : BaseController
    {
        public IActionResult Index()
        {
            return ReturnView("CustomJsonResultView", new CustomJsonResultModel());
        }


        public IActionResult SucessMessage()
        {
            return new SuccessMessageResult("Its superb sucess");
        }

        public IActionResult ErrorMessage()
        {
            return new ErrorMessageResult("Its superb error");
        }

        public IActionResult WarnMessage()
        {
            return new WarnMessageResult("Its superb warn");
        }

        public IActionResult Compose()
        {
            return new ComposeResult(new WarnMessageResult("Its superb Warn"), new ErrorMessageResult("Its superb error"));
        }

        public IActionResult ReplaceHtml()
        {
            return new ReplaceHtmlResult("#replaceDiv", Url.Action("GetTime"));
        }

        public IActionResult AppendHtml()
        {
            return new AppendHtmlResult("#appendDiv", Url.Action("GetTime"));
        }

        public IActionResult OpenNewWindow()
        {
            return new OpenNewWindowResult("www.google.com");
        }

        public ActionResult GetTime()
        {
            return ReturnView("TimeView", DateTime.Now.Second);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.BaseApi;

namespace NewsBoard.Tools
{
    public abstract class BaseController : Controller
    {

        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }


        public ActionResult ReturnView(string viewName, object model)
        {
            if (IsAjaxRequest(Request))
                return PartialView(viewName, model);

            return View(viewName, model);
        }

        public NewsBoardUrlHelper NewsBoardUrlHelper
        {
            get
            {
                return Url.NewsBoardUrlHelper();
            }
        }

        public int UserId
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return UserId > 1;
            }
        }

        public bool IsAnonymous
        {
            get
            {
                return UserId == 0;
            }
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && context.Exception is NeedAuthenticationException)
            {
                context.Result = new ComposeResult(
                    new ErrorMessageResult(context.Exception.Message),
                    new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("", "Home", "Index")),
                    new ReplaceHtmlResult("#UserMenu", Url.NewsBoardUrlHelper().Action("User", "UserMenu", "Index"))
                    );

                context.ExceptionHandled = true;
            }
            else if (context.Exception != null && context.Exception is BusinessLogicException)
            {
                context.Result = new ErrorMessageResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
             {
                var fatalMessage = "Application error, reloading the app.";
                TempData.Add("FatalMessage", fatalMessage);

                context.Result = new ComposeResult(
                    new FatalResult(fatalMessage),
                    new LoadUrlResult(Url.NewsBoardUrlHelper().Action("", "Home", "Index")));

                context.ExceptionHandled = true;
            }
        }
    }
}

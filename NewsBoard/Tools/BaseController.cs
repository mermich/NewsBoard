using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NewBoardRestApi.BaseApi;
using ServerSideSpaTools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.Tools
{
    public abstract class BaseController : AutoSwapReponseController
    {

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

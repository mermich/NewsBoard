using ApiTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return HttpContext.Session.GetInt32("UserId").GetValueOrDefault(BaseAuthenticatedApi.UnAuthenticatedUserId);
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return UserId != BaseAuthenticatedApi.UnAuthenticatedUserId;
            }
        }

        public bool IsAnonymous
        {
            get
            {
                return UserId == BaseAuthenticatedApi.UnAuthenticatedUserId;
            }
        }

        public bool IsFirstVisit
        {
            get
            {
                return UserId == BaseAuthenticatedApi.UnAuthenticatedUserId;
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
                var fatalMessage = "Erreur dans l'application, retour a la page d'accueil.";

                if(!TempData.ContainsKey("FatalMessage"))
                    TempData.Add("FatalMessage", fatalMessage);

                context.Result = new ComposeResult(
                    new FatalResult(fatalMessage),
                    new LoadUrlResult(Url.NewsBoardUrlHelper().Action("", "Home", "Index")));

                context.ExceptionHandled = false;
            }
        }

        public ActionResult ReturnReplaceMainView(ReplaceMainHtmlResult res)
        {
            if (IsAjaxRequest)
            {
                return res;
            }

            else
            {
                return Redirect(res.Action);
            }
        }
    }
}

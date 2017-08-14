using Microsoft.AspNetCore.Mvc.Filters;
using ApiTools;
using ServerSideSpaTools.JsonResult;
using System;

namespace ServerSideSpaTools
{
    public abstract class BaseController : AutoSwapReponseController
    {
        public abstract CustomJsonResult OnNeedAuthenticationException();

        public abstract CustomJsonResult OnBusinessLogicException(BusinessLogicException ex);

        public abstract CustomJsonResult OnApplicationException(Exception ex);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && context.Exception is NeedAuthenticationException)
            {
                context.Result = OnNeedAuthenticationException();
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null && context.Exception is BusinessLogicException)
            {
                context.Result = OnBusinessLogicException(context.Exception as BusinessLogicException);
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                context.Result = OnApplicationException(context.Exception);
                context.ExceptionHandled = true;
            }
        }
    }
}

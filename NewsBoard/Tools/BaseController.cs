using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
    }
}

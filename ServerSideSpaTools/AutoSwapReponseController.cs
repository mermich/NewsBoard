using Microsoft.AspNetCore.Mvc;
using System;

namespace ServerSideSpaTools
{
    public abstract class AutoSwapReponseController : Controller
    {

        public bool IsAjaxRequest
        {
            get
            {
                if (Request == null)
                    throw new ArgumentNullException("request");

                if (Request.Headers != null)
                    return Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                return false;
            }
        }


        public ActionResult ReturnView(string viewName, object model)
        {
            if (IsAjaxRequest)
                return PartialView(viewName, model);

            return View(viewName, model);
        }
    }
}

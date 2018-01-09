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
                {
                    throw new ArgumentNullException("request");
                }
                else if (Request.Headers != null)
                {
                    return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                }
                else
                {
                    return false;
                }
            }
        }


        public ActionResult ReturnView(string viewName, object model)
        {
            if (IsAjaxRequest)
            {
                return PartialView(viewName, model);
            }

            else
            {
                var wasThere = HttpContext.Request.Cookies["wasThere"];
                return View(viewName, model);
            }
        }
    }
}

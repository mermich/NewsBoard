using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiscoverWebSiteApi;
using WebAppUtilities;
using WebAppUtilities.JsonResult;

namespace DiscoverWebSite.Controllers
{
    public class HomeController : AutoSwapReponseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetDiscoverWebSite(string url)
        {
            return new ReplaceMainHtmlResult(Url.Action("DiscoverWebSite"));
        }


        public IActionResult DiscoverWebSite(string url)
        {
            var api = new LookupWebSiteApi();
            var model = api.GetWebSiteDetails(url);
            return ReturnView("DiscoverWebSite", model);
        }

        public IActionResult SyndicationContent(string url)
        {
            var api = new SyndicationApi();
            var model = api.GetSyndication(url);
            return ReturnView("SyndicationContent", model);
        }
    }
}

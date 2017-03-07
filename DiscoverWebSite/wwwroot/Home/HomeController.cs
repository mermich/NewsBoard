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

        [HttpPost]
        public JsonResult GetDiscoverWebSite(string webSiteUrl)
        {
            return new ReplaceMainHtmlResult(Url.Action("DiscoverWebSite", new { webSiteUrl = webSiteUrl }));
        }
        
        public IActionResult DiscoverWebSite(string webSiteUrl)
        {
            var api = new LookupWebSiteApi();
            var model = api.GetWebSiteDetails(webSiteUrl);
            return ReturnView("DiscoverWebSite", model);
        }

        public IActionResult SyndicationContent(string feedUrl)
        {
            var api = new SyndicationApi();
            var model = api.GetSyndication(feedUrl);
            return ReturnView("SyndicationContent", model);
        }
    }
}

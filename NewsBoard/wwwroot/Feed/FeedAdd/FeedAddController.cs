using DiscoverWebSiteApi;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedAdd
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedAddController : BaseController
    {
        public IActionResult Index()
        {
            return ReturnView("FeedAddView", null);
        }

        public IActionResult GetPreview(string urlToDiscover)
        {
            return new ReplaceHtmlResult("#preview", Url.Action("Preview", "FeedAdd", new { urlToDiscover = urlToDiscover }));
        }

        public IActionResult Preview(string urlToDiscover)
        {
            var preview = new LookupWebSiteApi().GetWebSiteDetails(urlToDiscover);
            var syndication = new SyndicationApi().GetSyndication(preview.SyndicationAdress);
            var model = new NewsBoard.wwwroot.Feed.FeedAdd.FeedAddPreview
            {
                WebSiteDetails = preview,
                SyndicationContent = syndication
            };
            return ReturnView("FeedAddPreviewView", model);
        }

        [HttpPost]
        public IActionResult CreateSubscription(WebSiteDetails details)
        {
            var feed = new FeedApi(UserId).CreateSubscriptionAndSubScribe(details.SyndicationAdress);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("feed", "FeedList", "Index")),
                new SuccessMessageResult("Feed Created")
                );
        }
    }
}
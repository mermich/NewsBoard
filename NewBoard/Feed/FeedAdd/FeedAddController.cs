using ApiTools;
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
    public partial class FeedAddController : BaseController
    {
        FeedApi feedApi;

        public FeedAddController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            return ReturnView("FeedAddView", null);
        }

        public virtual IActionResult GetPreview(string urlToDiscover)
        {
            return new ReplaceHtmlResult("#preview", Url.Action("Preview", "FeedAdd", new { urlToDiscover = urlToDiscover }));
        }

        public virtual IActionResult Preview(string urlToDiscover)
        {
            var preview = new LookupWebSiteApi().GetWebSiteDetails(urlToDiscover);
            var syndication = new SyndicationApi().GetSyndication(preview.SyndicationUrl);
            var model = new FeedAddPreview
            {
                WebSiteDetails = preview,
                SyndicationContent = syndication
            };
            return ReturnView("FeedAddPreviewView", model);
        }

        [HttpPost]
        public virtual IActionResult CreateSubscription(WebSiteDetails details)
        {
            var feed = feedApi.CreateSubscriptionAndSubScribe(details.SyndicationUrl);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.UserFeedListAction),
                new SuccessMessageResult("Feed Created")
                );
        }
    }
}
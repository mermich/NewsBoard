using Microsoft.AspNetCore.Mvc;
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
            return ReturnView("FeedAddView",null);
        }

        public IActionResult GetPreview(string urlToDiscover)
        {
            return new ReplaceHtmlResult("#preview", Url.Action("Preview", "FeedAdd", new { urlToDiscover = urlToDiscover }));
        }

        public IActionResult Preview(string urlToDiscover)
        {
            var preview = new FeedApi(UserId).GetPreview(urlToDiscover);
            return ReturnView("FeedAddPreviewView", preview);
        }

        [HttpPost]
        public IActionResult CreateSubscription(FeedVMPreview addFeed)
        {
            var feed = new FeedApi(UserId).CreateSubscriptionAndSubScribe(addFeed.SyndicationUrl);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("feed", "FeedList", "Index")),
                new SuccessMessageResult("Feed Created")
                );
        }
    }
}
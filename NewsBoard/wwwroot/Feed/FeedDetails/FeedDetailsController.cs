using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;

namespace NewsBoard.wwwroot.Feed.FeedDetails
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedDetailsController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            var feed = feedRepo.GetFeed(feedId);
            
            return ReturnView("FeedDetailsView", feed);
        }

        public IActionResult Subscribe(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.SubscribeFeed(feedId);

            return new SuccessMessageResult("Subscribed");
        }

        public IActionResult Report(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.ReportFeed(feedId);

            return new SuccessMessageResult("Reported");
        }

        public IActionResult Unsubscribe(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.UnSubscribeFeed(feedId);

            return new SuccessMessageResult("UnSubscribed");
        }

        public IActionResult RefreshFeed(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.Refresh(feedId);

            return new SuccessMessageResult("Refreshed");
        }
    }
}
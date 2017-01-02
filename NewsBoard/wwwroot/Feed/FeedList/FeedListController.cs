using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using System.Linq;
using NewBoardRestApi.Api;

namespace NewsBoard.wwwroot.Feed.FeedList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedListController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            var model = feedRepo.ListFeed();

            return ReturnView("FeedListView", model);
        }

        public IActionResult Subscribe(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.SubscribeFeed(feedId);

            return new ReplaceHtmlResult("#UserSubscription", Url.Action("Index", "UserSubscription"));
        }

        public IActionResult Unsubscribe(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.UnSubscribeFeed(feedId);

            return new ReplaceHtmlResult("#UserSubscription", Url.Action("Index", "UserSubscription"));
        }

        public IActionResult Report(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.ReportFeed(feedId);

            return new SuccessMessageResult("Reported");
        }

        public IActionResult ShowDetails(int feedId)
        {
            var feedRepo = new FeedApi(HttpContext.Session.Id);
            feedRepo.OpenFeed(feedId);

            return new ReplaceHtmlResult("#page", Url.Action("Index", "FeedDetails", new { feedId = feedId }));
        }
    }
}
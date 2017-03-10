using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using System.Collections.Generic;
using NewBoardRestApi.FeedApi;
using WebAppUtilities.JsonResult;

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
            var feedRepo = new FeedApi(UserId);
            var feed = feedRepo.GetFeed(feedId);

            return ReturnView("FeedDetailsView", feed);
        }

        public IActionResult Subscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.SubscribeFeed(feedId);

            return new SuccessMessageResult("Subscribed");
        }

        public IActionResult Report(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.ReportFeed(feedId);

            return new SuccessMessageResult("Reported");
        }

        public IActionResult Unsubscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.UnSubscribeFeed(feedId);

            return new SuccessMessageResult("UnSubscribed");
        }

        public IActionResult RefreshFeed(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.Refresh(feedId);

            return new ComposeResult(
                new ReplaceHtmlResult("#articleList", NewsBoardUrlHelper.Action("Article", "ArticleList", "Index", new ArticleVMSearch() { Feeds = new List<int> { feedId } })),
                new SuccessMessageResult("Flux Refreshed")
            );
        }
    }
}
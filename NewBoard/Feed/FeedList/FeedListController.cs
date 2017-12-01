using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedListController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index(FeedVMSearch filter, FeedVMListOptions options)
        {
            var feedRepo = new FeedApi(UserId);
            var model = feedRepo.ListFeed(filter);
            model.Options = options;

            return ReturnView("FeedListView", model);
        }

        public IActionResult Open(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            var feed = feedRepo.GetFeed(feedId);

            if (IsAjaxRequest)
            {
                // Opens the article and should also update stats.
                return new ComposeResult(
                    new OpenNewWindowResult(feed.WebSiteUrl),
                    new WarnMessageResult("Ouverture dans une nouvelle fenetre."));

            }
            else
            {
                return new RedirectResult(feed.WebSiteUrl);
            }
        }


        public IActionResult FeedAction(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            var model = feedRepo.GetFeed(feedId);
            return ReturnView("FeedAction", model);
        }

        public IActionResult Subscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.SubscribeFeed(feedId);


            return new ComposeResult(
               new SuccessMessageResult("Subscribed"),
               new ReplaceHtmlResult("#UserSubscription", NewsBoardUrlHelper.Action("Feed", "UserSubscription", "Index")),
               new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }

        public IActionResult Unsubscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.UnSubscribeFeed(feedId);

            return new ComposeResult(
                new SuccessMessageResult("Unsubscribed"),
                new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }

        public IActionResult Report(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.ReportFeed(feedId);

            return new ComposeResult(
                  new SuccessMessageResult("Reported"),
                  new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                  new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }


        public IActionResult StopDisplay(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.StopDisplayFeed(feedId);

            return new ComposeResult(
                  new SuccessMessageResult("StopDisplay"),
                  new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                  new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }



        public IActionResult ShowDetails(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.OpenFeed(feedId);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedDetails", "Index", new { feedId = feedId }));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetEdit(int feedId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedEdit", "Index", new { feedId = feedId }));
        }
    }
}
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
    public partial class FeedListController : BaseController
    {
        FeedApi feedApi;

        public FeedListController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }
        

        public virtual IActionResult Index(FeedVMSearch filter, FeedVMListOptions options)
        {            
            var model = feedApi.ListFeed(filter);
            model.Options = options;

            return ReturnView("FeedListView", model);
        }

        public virtual IActionResult Open(int feedId)
        {
            var feed = feedApi.GetFeed(feedId);

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


        public virtual IActionResult FeedAction(int feedId)
        {
            var model = feedApi.GetFeed(feedId);
            return ReturnView("FeedAction", model);
        }

        public virtual IActionResult Subscribe(int feedId)
        {
            feedApi.SubscribeFeed(feedId);


            return new ComposeResult(
               new SuccessMessageResult("Subscribed"),
               new ReplaceHtmlResult("#UserSubscription", NewsBoardUrlHelper.Action("Feed", "UserSubscription", "Index")),
               new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }

        public virtual IActionResult Unsubscribe(int feedId)
        {
            feedApi.UnSubscribeFeed(feedId);

            return new ComposeResult(
                new SuccessMessageResult("Unsubscribed"),
                new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }

        public virtual IActionResult Report(int feedId)
        {
            feedApi.ReportFeed(feedId);

            return new ComposeResult(
                  new SuccessMessageResult("Reported"),
                  new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                  new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }


        public virtual IActionResult StopDisplay(int feedId)
        {
            feedApi.StopDisplayFeed(feedId);

            return new ComposeResult(
                  new SuccessMessageResult("StopDisplay"),
                  new RemoveHtmlResult("[article-feed='" + feedId + "']"),
                  new ReplaceHtmlResult("[feed='" + feedId + "']", NewsBoardUrlHelper.Action("Feed", "FeedList", "FeedAction", new { feedId = feedId })));
        }



        public virtual IActionResult ShowDetails(int feedId)
        {
            feedApi.OpenFeed(feedId);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedDetails", "Index", new { feedId = feedId }));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetEdit(int feedId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedEdit", "Index", new { feedId = feedId }));
        }
        
        public virtual IActionResult Refresh(int feedId)
        {
            feedApi.RefreshFeedArticles(feedId);

            return new SuccessMessageResult("Updated");
        }
    }
}
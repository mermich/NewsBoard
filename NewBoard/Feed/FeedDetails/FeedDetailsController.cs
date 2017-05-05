using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;
using System.Collections.Generic;

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

        public IActionResult RefreshFeed(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.RefreshFeedArticles(feedId);

            return new ComposeResult(
                new ReplaceHtmlResult("#articleList", NewsBoardUrlHelper.Action("Article", "ArticleList", "Index", new ArticleVMSearch() { Feeds = new List<int> { feedId } })),
                new SuccessMessageResult("Flux Refreshed")
            );
        }
    }
}
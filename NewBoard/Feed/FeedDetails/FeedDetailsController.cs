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
    public partial class FeedDetailsController : BaseController
    {
        FeedApi feedApi;

        public FeedDetailsController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }


        
        public virtual IActionResult Index(int feedId)
        {
            var feed = feedApi.GetFeed(feedId);

            return ReturnView("FeedDetailsView", feed);
        }

        
        public virtual IActionResult RefreshFeed(int feedId)
        {
            feedApi.RefreshFeedArticles(feedId);

            return new ComposeResult(
                new ReplaceHtmlResult("#articleList", NewsBoardUrlHelper.Action("Article", "ArticleList", "Index", new ArticleVMSearch() { Feeds = new List<int> { feedId } })),
                new SuccessMessageResult("Flux Refreshed")
            );
        }
    }
}
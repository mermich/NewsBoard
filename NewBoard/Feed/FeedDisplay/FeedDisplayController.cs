using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedEdit
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedDisplayController : BaseController
    {
        FeedApi feedApi;


        public FeedDisplayController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }


        [ResponseCache(Duration = 300)]
        public IActionResult Index(int feedId)
        {
            var model = feedApi.GetFeedEdit(feedId);

            return ReturnView("FeedEditView", model);
        }
    }
}
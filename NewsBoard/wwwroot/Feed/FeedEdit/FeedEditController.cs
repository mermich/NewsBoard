using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using System.Linq;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.Feed.FeedEdit
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedEditController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(int feedId)
        {
            var model = new FeedApi(UserId).GetFeedEdit(feedId);

            return ReturnView("FeedEditView", model);
        }


        public IActionResult Update(FeedEditVM feed)
        {
            new FeedApi(UserId).SaveFeed(feed);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedList", "Index")),
                new SuccessMessageResult("Tag Created")
                );
        }
    }
}
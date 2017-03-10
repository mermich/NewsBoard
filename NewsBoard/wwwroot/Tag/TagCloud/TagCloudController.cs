using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCloudController : BaseController
    {

        public IActionResult Index()
        {
            var api = new TagApi(UserId);
            var model = api.GetUsedTags();

            return ReturnView("TagCloudView", model);
        }

        public ActionResult GetBrowseByTag(int id)
        {
            var tagModel = new TagApi(UserId).GetTag(id);
            var filter = new FeedVMSearch();
            filter.Tags.Add(id);

            var options = new FeedVMListOptions { Heading = "Flux du tag:" + tagModel.Label };

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.FeedListAction(filter, options));
        }
    }
}
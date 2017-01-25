using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

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
            var filter = new FeedVMListFilter();
            filter.Tags.Add(id);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedList", "Index", filter));
        }
    }
}
using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagListController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var api = new TagApi(UserId);
            var model = api.GetTags();

            return ReturnView("TagListView", model);
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetEdit(int tagId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagEdit", "Index", new { tagId = tagId }));
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagCreate", "Index"));
        }
    }
}
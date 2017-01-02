using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagListController : BaseController
    {

        public IActionResult Index()
        {
            var api = new TagApi();
            var model =api.GetTags();
            
            return ReturnView("TagListView", model);
        }

        public ActionResult GetEdit(int tagId)
        {
            return new ReplaceMainHtmlResult(Url.Action("Edit", "TagEdit", new { tagId = tagId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(Url.Action("Index", "TagCreate"));
        }
    }
}
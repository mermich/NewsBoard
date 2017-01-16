using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    [Authorize(Roles = "Administrator")]
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
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagEdit","Index", new { tagId = tagId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagCreate", "Index"));
        }
    }
}
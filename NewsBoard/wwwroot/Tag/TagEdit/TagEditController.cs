using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagEditController : BaseController
    {

        public IActionResult Index(int tagId)
        {
            var api = new TagApi(UserId);
            var model = api.GetEditTag(tagId);
            
            return ReturnView("TagEditView", model);
        }

        public ActionResult Update(TagEditVM model)
        {
            var api = new TagApi(UserId);
            api.CreateTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagList", "Index")),
                new SuccessMessageResult("Tag Updated")
                );
        }
    }
}
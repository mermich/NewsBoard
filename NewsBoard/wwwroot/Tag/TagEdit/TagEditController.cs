using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.TagApi;
using WebAppUtilities.JsonResult;

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
            api.SaveTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagList", "Index")),
                new SuccessMessageResult("Tag Updated")
                );
        }
    }
}
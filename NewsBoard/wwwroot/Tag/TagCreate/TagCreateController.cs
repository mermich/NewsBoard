using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCreateController : BaseController
    {

        public IActionResult Index()
        {
            var api = new TagApi(UserId);
            var model = api.GetNewCreateTag();
            
            return ReturnView("TagCreateView", model);
        }

        public ActionResult Create(TagEditVM model)
        {
            var api = new TagApi(UserId);
            api.CreateTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagList","Index")),
                new SuccessMessageResult("Tag Created")
                );
        }
    }
}
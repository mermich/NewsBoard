using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCreateController : BaseController
    {

        public IActionResult Index()
        {
            var api = new TagApi();
            var model = api.GetNewCreateTag();
            
            return ReturnView("TagCreateView", model);
        }

        public ActionResult Create(TagEditVM model)
        {
            var api = new TagApi();
            api.CreateTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(Url.Action("Index","TagList")),
                new SuccessMessageResult("Tag Created")
                );
        }
    }
}
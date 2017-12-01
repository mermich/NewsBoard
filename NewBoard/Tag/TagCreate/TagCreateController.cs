using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCreateController : BaseController
    {
        [ResponseCache(Duration = 300)]
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
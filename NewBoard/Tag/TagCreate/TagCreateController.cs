using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public partial class TagCreateController : BaseController
    {
        TagApi tagApi;

        public TagCreateController(TagApi tagApi)
        {
            this.tagApi = tagApi;
        }


        
        public virtual IActionResult Index()
        {
            var model = tagApi.GetNewCreateTag();
            
            return ReturnView("TagCreateView", model);
        }
        
        public virtual ActionResult Create(TagEditVM model)
        {
            tagApi.CreateTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagList","Index")),
                new SuccessMessageResult("Tag Created")
                );
        }
    }
}
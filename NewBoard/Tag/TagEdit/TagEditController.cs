using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public partial class TagEditController : BaseController
    {
        TagApi tagApi;

        public TagEditController(TagApi tagApi)
        {
            this.tagApi = tagApi;
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index(int tagId)
        {
            var model = tagApi.GetEditTag(tagId);
            
            return ReturnView("TagEditView", model);
        }

        public virtual ActionResult Update(TagEditVM model)
        {
            tagApi.SaveTag(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagList", "Index")),
                new SuccessMessageResult("Tag Updated")
                );
        }
    }
}
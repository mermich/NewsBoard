using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public partial class TagListController : BaseController
    {
        TagApi tagApi;

        public TagListController(TagApi tagApi)
        {
            this.tagApi = tagApi;
        }

        
        public virtual IActionResult Index()
        {
            var model = tagApi.GetTags();

            return ReturnView("TagListView", model);
        }

        
        public virtual ActionResult GetEdit(int tagId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagEdit", "Index", new {  tagId })).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Tag", "TagCreate", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}
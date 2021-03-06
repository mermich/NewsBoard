using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.TagApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public partial class TagCloudController : BaseController
    {
        TagApi tagApi;

        public TagCloudController(TagApi tagApi)
        {
            this.tagApi = tagApi;
        }


        public virtual IActionResult Index()
        {
            var model = tagApi.GetUsedTags();

            return ReturnView("TagCloudView", model);
        }


        public virtual ActionResult GetArticlesByTag(int id)
        {
            var tagModel = tagApi.GetTag(id);
            var filter = new ArticleVMSearch();
            filter.Tags.Add(id);

            var options = new ArticleVMListOptions("Articles du Tag : " + tagModel.Label);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.ArticleListAction(filter, options)).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}
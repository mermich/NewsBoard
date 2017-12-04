using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.TagApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCloudController : BaseController
    {
        TagApi tagApi;

        public TagCloudController(TagApi tagApi)
        {
            this.tagApi = tagApi;
        }

        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var model = tagApi.GetUsedTags();

            return ReturnView("TagCloudView", model);
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetArticlesByTag(int id)
        {
            var tagModel = tagApi.GetTag(id);
            var filter = new ArticleVMSearch();
            filter.Tags.Add(id);

            var options = new ArticleVMListOptions { Heading = "Articles du Tag : " + tagModel.Label };

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.ArticleListAction(filter, options));
        }
    }
}
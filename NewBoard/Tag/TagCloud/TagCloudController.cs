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

        public IActionResult Index()
        {
            var api = new TagApi(UserId);
            var model = api.GetUsedTags();

            return ReturnView("TagCloudView", model);
        }

        public ActionResult GetArticlesByTag(int id)
        {
            var tagModel = new TagApi(UserId).GetTag(id);
            var filter = new ArticleVMSearch();
            filter.Tags.Add(id);

            var options = new ArticleVMListOptions { Heading = "Articles du Tag : " + tagModel.Label };

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.ArticleListAction(filter, options));
        }
    }
}
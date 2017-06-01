using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using ServerSideSpaTools.JsonResult;
using NewBoardRestApi.ArticleApi;

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
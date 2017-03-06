using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.Home
{
    /// <summary>
    /// Handles everything for the home page.
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
        }

        public IActionResult Index()
        {
            return ReturnView("IndexView", new HomeModel());
        }

        public IActionResult GetUserArticles()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserArticleListAction);
        }

        public IActionResult GetAllArticleList()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllArticleListAction);
        }

        public IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserFeedListAction);
        }

        public IActionResult GetAllFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllFeedListAction);
        }

        public IActionResult GetAddFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedAdd", "Index"));
        }

        public IActionResult GetAboutPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Misc", "About", "Index"));
        }

        public IActionResult GetTagListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Tag", "TagList", "Index"));
        }

        public IActionResult GetGroupListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Group", "GroupList", "Index"));
        }

        public IActionResult GetPermissionListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Permission", "PermissionList", "Index"));
        }

        public IActionResult GetUserListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("User", "UserList", "Index"));
        }

        public IActionResult GetSimpleGetAction()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Controls", "SimpleGetAction", "Index"));
        }


        public IActionResult GetCustomJsonResult()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Controls", "CustomJsonResult", "Index"));
        }

        public IActionResult GetCascadingData()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Controls", "CascadingData", "Index"));
        }
        
    }
}
using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api.Model;

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

        public IActionResult GetUserHomePage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "Index", new ArticleVMListFilter()));
        }

        public IActionResult GetUserArticles()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "UserSubscription", new ArticleVMListFilter()));
        }

        public IActionResult GetAllArticleList()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "Index", new ArticleVMListFilter { OnlyUserSubscription = false, MaxItems = 50 }));
        }

        public IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedList", "UserSubscription", new FeedVMListFilter()));
        }

        public IActionResult GetAllFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedList", "Index", new FeedVMListFilter { OnlyUserSubscription = false, HideReported = false, MaxItems = 50 }));
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
    }
}
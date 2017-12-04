using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Home
{
    /// <summary>
    /// Handles everything for the home page.
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
           var tt = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        }
        
        public IActionResult Index()
        {
            return ReturnView("IndexView", new HomeModel());
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetUserArticles()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserArticleListAction);
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetArticleSearch()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllArticleListAction);
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserFeedListAction);
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetFeedSearch()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllFeedListAction);
        }        


        [ResponseCache(Duration = 300)]
        public IActionResult GetAddFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedAdd", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetAboutPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Misc", "About", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetTagListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Tag", "TagList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetGroupListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Group", "GroupList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetPermissionListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Permission", "PermissionList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public IActionResult GetUserListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("User", "UserList", "Index"));
        }
    }
}
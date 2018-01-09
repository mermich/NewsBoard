using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Home
{
    /// <summary>
    /// Handles everything for the home page.
    /// </summary>
    public partial class HomeController : BaseController
    {
        public HomeController() : base()
        {

        }

        public virtual IActionResult Index()
        {
            return ReturnView("IndexView", new HomeModel());
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetUserArticles()
        {
            return ReturnReplaceMainView(new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserArticleListAction));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetArticleSearch()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllArticleListAction);
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserFeedListAction);
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetAllFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllFeedListAction);
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetAddFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedAdd", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetAboutPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Misc", "About", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetTagListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Tag", "TagList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetGroupListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Group", "GroupList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetPermissionListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Permission", "PermissionList", "Index"));
        }

        [ResponseCache(Duration = 300)]
        public virtual IActionResult GetUserListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("User", "UserList", "Index"));
        }
    }
}
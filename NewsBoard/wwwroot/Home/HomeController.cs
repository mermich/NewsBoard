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

        public IActionResult GetUserhomePage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "Index", new ArticleListFilterVM()));
        }

        public IActionResult GetUserArticles()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "Index", new ArticleListFilterVM()));
        }

        public IActionResult GetAllArticleList()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Article", "ArticleList", "Index", new ArticleListFilterVM { OnlyUserSubscription = false, MaxItems = 50 }));
        }

        public IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedList", "Index", new FeedListFilterVM()));
        }

        public IActionResult GetAllFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedList", "Index", new FeedListFilterVM { OnlyUserSubscription = false, HideReported = false, MaxItems = 50 }));
        }

        public IActionResult GetAddFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedAdd", "Index"));
        }

        public IActionResult GetAboutPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Misc", "About", "Index"));
        }
    }
}
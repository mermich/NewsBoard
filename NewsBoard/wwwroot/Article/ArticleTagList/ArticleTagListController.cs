using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NewBoardRestApi.Api;

namespace NewsBoard.wwwroot.Article.ArticleList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public class ArticleTagListController : BaseController
    {

        public IActionResult Index(bool showHidden = false)
        {
            var articleRepo = new ArticleApi(HttpContext.Session.Id);
            var model = articleRepo.GetLatestArticlesForUser(showHidden);

            return ReturnView("ArticleListView", model);
        }

        public IActionResult Open(int feedId, bool showHidden = false)
        {
            var articles = new ArticleApi(HttpContext.Session.Id).GetLatestArticlesForUserAndFeed(feedId, showHidden);

            return ReturnView("ArticleListView", articles);
        }

        public IActionResult Hide(int articleId)
        {
            var articleApi = new ArticleApi(HttpContext.Session.Id);
            articleApi.HideArticle(articleId);

            var articles = articleApi.GetLatestArticles();

            return ReturnView("ArticleListView", articles);
        }
    }
}
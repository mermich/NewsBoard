using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.Article.ArticleList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public class ArticleListController : BaseController
    {
        public IActionResult Index(ArticleListFilterVM filter)
        {
            var articleRepo = new ArticleApi(UserId);
            var model = articleRepo.GetArticles(filter);

            return ReturnView("ArticleListView", model);
        }

        public IActionResult Hide(int articleId)
        {
            var articleApi = new ArticleApi(UserId);
            articleApi.HideArticle(articleId);

            var articles = articleApi.GetArticles(new ArticleListFilterVM());

            return ReturnView("ArticleListView", articles);
        }
    }
}
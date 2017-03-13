using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Article.ArticleList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public class ArticleListController : BaseController
    {
        public IActionResult Index(ArticleVMSearch filter, ArticleVMListOptions options)
        {
            var articleRepo = new ArticleApi(UserId);
            var model = articleRepo.GetArticles(filter);
            model.Options = options;

            return ReturnView("ArticleListView", model);
        }

        public IActionResult Open(int articleId)
        {
            var articleApi = new ArticleApi(UserId);
            var article = articleApi.OpenArticle(articleId);

            // Opens the article and should also update stats.
            return new ComposeResult(
                new OpenNewWindowResult(article.Url),
                new WarnMessageResult("Ouverture dans une nouvelle fenetre."));
        }

        public IActionResult Hide(int articleId)
        {
            var articleApi = new ArticleApi(UserId);
            articleApi.HideArticle(articleId);
            
            return new ComposeResult(
                new HideHtmlResult("#article-" + articleId),
                new SuccessMessageResult("Cet article ne sera plus affiche."));
        }

    }
}
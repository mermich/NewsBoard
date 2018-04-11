using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using ServerSideSpaTools.JsonResult;
using NewBoardRestApi.FeedApi;

namespace NewsBoard.wwwroot.Article.ArticleList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public partial class ArticleListController : BaseController
    {
        ArticleApi articleApi;
        FeedApi feedApi;

        public ArticleListController(ArticleApi articleApi, FeedApi feedApi)
        {
            this.articleApi = articleApi;
            this.feedApi = feedApi;
        }
        
        public virtual IActionResult Index(ArticleVMSearch filter, ArticleVMListOptions options)
        {
            var model = articleApi.GetArticles(filter);
            model.Options = options;

            return ReturnView("ArticleListView", model);
        }

        public virtual IActionResult Open(int articleId)
        {
            var article = articleApi.OpenArticle(articleId);

            // Opens the article and should also update stats.
            if (IsAjaxRequest)
            {
                // Opens the article and should also update stats.
                return new ComposeResult(
                new OpenNewWindowResult(article.Url),
                new WarnMessageResult("Ouverture dans une nouvelle fenetre."));

            }
            else
            {
                return new RedirectResult(article.Url);
            }
        }

        public virtual IActionResult Hide(int articleId)
        {
            articleApi.HideArticle(articleId);

            return new ComposeResult(
                new HideHtmlResult("[article='" + articleId + "']"),
                new SuccessMessageResult("Cet article ne sera plus affiche."));
        }




        public virtual ActionResult GetArticlesByFeed(int id)
        {
            var filter = new ArticleVMSearch();
            filter.Feeds.Add(id);

            var feed = feedApi.GetFeed(id);

            var options = new ArticleVMListOptions("Articles du flux : " + feed.Description);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.ArticleListAction(filter, options)).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}
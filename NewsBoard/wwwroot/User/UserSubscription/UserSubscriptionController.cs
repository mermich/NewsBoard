using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;

namespace NewsBoard.wwwroot.User.UserSubscription
{
    [Area("User")]
    public class UserSubscriptionController : BaseController
    {
        public IActionResult Index()
        {
            var articleRepo = new ArticleApi(UserId);
            var articles = articleRepo.GetArticles();

            return ReturnView("UserSubscriptionView", articles);
        }

        public ActionResult Open(int articleId)
        {
            var articleRepo = new ArticleApi(UserId);
            articleRepo.OpenArticle(articleId);
            var article = articleRepo.GetArticle(articleId);

            // Opens the article and should also update stats.
            return new ComposeResult(
                new OpenNewWindowResult(article.Url),
                new HideHtmlResult("#article-" + articleId),
                new WarnMessageResult("Ouverture de l'article dans une nouvelle fenetre."));
        }

        public ActionResult Hide(int articleId)
        {
            var articleRepo = new ArticleApi(UserId);
            articleRepo.HideArticle(articleId);

            return new ComposeResult(
                new HideHtmlResult("#article-" + articleId),
                new WarnMessageResult("l'article ne sera plus afiche."));
        }
    }
}
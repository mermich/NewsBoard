using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.Api;

namespace NewsBoard.wwwroot.Article.ArticleDetails
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public class ArticleDetailsController : BaseController
    {

        public IActionResult Index(int articleId)
        {
            return ReturnView("", null);
        }

        public IActionResult Open(int articleId)
        {
            var articleRepo = new ArticleApi(UserId);
            articleRepo.OpenArticle(articleId);

            return ReturnView("",null);
        }


        public IActionResult Hide(int articleId)
        {
            var articleRepo = new ArticleApi(UserId);
            articleRepo.HideArticle(articleId);

            return ReturnView("", null);
        }
    }
}
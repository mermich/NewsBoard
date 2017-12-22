using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.Article.ArticleDetails
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public partial class ArticleDetailsController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index(int articleId)
        {
            return ReturnView("", null);
        }
    }
}
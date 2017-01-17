using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NewBoardRestApi.Api;
using System.Collections.Generic;
using NewBoardRestApi.Api.Model;

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
            var articleRepo = new ArticleApi(UserId);
            var model = articleRepo.GetArticles();

            return ReturnView("ArticleListView", model);
        }

        public IActionResult Open(int feedId)
        {
            var articles = new ArticleApi(UserId).GetArticles(new ArticleListFilterVM { Feeds = new List<int> { feedId }});

            return ReturnView("ArticleListView", articles);
        }

        public IActionResult Hide(int articleId)
        {
            var articleApi = new ArticleApi(UserId);
            articleApi.HideArticle(articleId);

            var articles = articleApi.GetArticles();

            return ReturnView("ArticleListView", articles);
        }
    }
}
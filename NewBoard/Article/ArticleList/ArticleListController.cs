﻿using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using ServerSideSpaTools.JsonResult;
using NewBoardRestApi.TagApi;

namespace NewsBoard.wwwroot.Article.ArticleList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Article")]
    public class ArticleListController : BaseController
    {
        ArticleApi articleApi;

        public ArticleListController(ArticleApi articleApi)
        {
            this.articleApi = articleApi;
        }

        [ResponseCache(Duration = 300)]
        public IActionResult Index(ArticleVMSearch filter, ArticleVMListOptions options)
        {
            var model = articleApi.GetArticles(filter);
            model.Options = options;

            return ReturnView("ArticleListView", model);
        }

        public IActionResult Open(int articleId)
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

        public IActionResult Hide(int articleId)
        {
            articleApi.HideArticle(articleId);

            return new ComposeResult(
                new HideHtmlResult("[article='" + articleId + "']"),
                new SuccessMessageResult("Cet article ne sera plus affiche."));
        }
    }
}
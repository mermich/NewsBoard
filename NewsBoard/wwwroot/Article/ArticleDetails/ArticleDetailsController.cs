﻿using NewsBoard.Tools;
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
    }
}
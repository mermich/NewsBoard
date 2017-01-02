using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.Api.Model;
using NewBoardRestApi.DataModel.Engine;

namespace NewBoardRestApi.Api
{
    [Route("api/[controller]")]
    public class ArticleApi : BaseAuthenticatedApi
    {
        public ArticleApi(string userToken = "") : base(userToken)
        {
        }



        [HttpGet("{id}")]
        public Model.ArticleVM GetArticle(int articleId)
        {
            return NewsBoardContext.Articles.SingleResult(a => a.Id == articleId).ToArticle();
        }


        [HttpGet()]
        public ArticleVMList GetLatestArticles()
        {
            return NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .Where(a => !a.UserArticles.Any() || !a.UserArticles.First(ua => ua.UserId == 1).IsHidden)
                .OrderBy(a => a.PublishDate)
                .Take(10)
                .ToArticleList();
        }

        [HttpGet("{showHidden}")]
        [Route("GetLatestArticlesForUser")]
        public ArticleVMList GetLatestArticlesForUser(bool showHidden)
        {
            var result = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .Where(a => !a.UserArticles.Any() || !a.UserArticles.First(ua => ua.User != null && ua.User.Id == currentUser.Id).IsHidden)
                .OrderBy(a => a.PublishDate)
                .Take(10)
                .ToArticleList();

            return result;
        }

        [HttpGet("{feedId}/{showHidden}")]
        [Route("GetLatestArticlesForFeed")]
        public ArticleVMList GetLatestArticlesForFeed(int feedId, bool showHidden)
        {
            UserArticle userArticleAlias = null;

            var result = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .Where(a => a.FeedId == feedId)
                .Where(a => userArticleAlias.IsHidden == false || userArticleAlias.IsHidden == showHidden)
                .OrderBy(a => a.PublishDate)
                .Take(10)
                .ToArticleList();

            return result;
        }


        [HttpGet("{feedId}/{showHidden}")]
        [Route("GetLatestArticlesForUserAndFeed")]
        public ArticleVMList GetLatestArticlesForUserAndFeed(int feedId, bool showHidden)
        {
            var result = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .Where(a => a.UserArticles.Any(ua=>ua.UserId == currentUser.Id && (ua.IsHidden ==false && ua.IsHidden == showHidden)))
                .Where(a => a.FeedId == feedId)
                .OrderBy(a => a.PublishDate)
                .Take(10)
                .ToArticleList();

            return result;
        }


        [HttpGet("{id}")]
        [Route("OpenArticle")]
        public virtual Model.ArticleVM OpenArticle(int id)
        {
            var article = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == id);

            var userArticle = NewsBoardContext.UserArticles.FirstOrDefault(ua => ua.ArticleId == id);
            if (userArticle == null)
            {
                userArticle = new UserArticle(currentUser, article);
                NewsBoardContext.UserArticles.Add(userArticle);
            }
            userArticle.IsOpened = true;
            NewsBoardContext.SaveChanges();


            return GetArticle(id);
        }

        [HttpGet("{id}")]
        [Route("HideArticle")]
        public virtual void HideArticle(int id)
        {
            var article = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == id);

            var userArticle = NewsBoardContext.UserArticles.FirstOrDefault(ua => ua.ArticleId == id);
            if (userArticle == null)
            {
                userArticle = new UserArticle(currentUser, article);
                NewsBoardContext.UserArticles.Add(userArticle);
            }
            userArticle.IsHidden = true;
            NewsBoardContext.SaveChanges();
        }

    }
}

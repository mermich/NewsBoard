using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using ApiTools;

namespace NewBoardRestApi.ArticleApi
{
    [Route("api/[controller]")]
    public class ArticleApi : BaseAuthenticatedApi
    {
        public ArticleApi(int userId) : base(userId)
        {
        }


        [HttpGet("{id}")]
        public ArticleVM GetArticle(int articleId)
        {
            return NewsBoardContext.Articles
                .Include(a => a.Feed).ThenInclude(f => f.UserFeeds)
                .Include(a => a.Feed).ThenInclude(f => f.WebSite)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == articleId)
                .ToArticle(UnAuthenticatedUserId);
        }


        public ArticleVMList GetArticles(ArticleVMSearch filter = null)
        {
            if (filter == null)
                filter = new ArticleVMSearch();

            var result = NewsBoardContext.Articles
                .Include(a => a.Feed).ThenInclude(f => f.WebSite)
                .Include(a => a.UserArticles)
                .Where(a => !filter.OnlyUserSubscription || !a.Feed.UserFeeds.Any(uf => uf.UserId == UserId && !uf.IsSubscribed))
                .Where(a => !filter.HideReported || !a.Feed.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsReported))
                .Where(a => !filter.Feeds.Any() || filter.Feeds.Contains(a.FeedId))
                .OrderByDescending(a => a.PublishDate)
                .Take(filter.MaxItems)
                .ToArticleList(UserId);

            return result;
        }


        [HttpGet("{id}")]
        [Route("OpenArticle")]
        public virtual ArticleVM OpenArticle(int id)
        {
            var article = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == id);

            var userArticle = NewsBoardContext.UserArticles.FirstOrDefault(ua => ua.ArticleId == id);
            if (userArticle == null)
            {
                userArticle = new UserArticle(UserId, article);
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
            if (IsAnonymous)
            {
                throw new BusinessLogicException("Seuls les utilisateurs authentifies peuvent masquer des articles.");
            }

            var article = NewsBoardContext.Articles
                .Include(a => a.Feed)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == id);

            var userArticle = NewsBoardContext.UserArticles.FirstOrDefault(ua => ua.ArticleId == id);
            if (userArticle == null)
            {
                userArticle = new UserArticle(UserId, article);
                NewsBoardContext.UserArticles.Add(userArticle);
            }
            userArticle.IsHidden = true;
            NewsBoardContext.SaveChanges();
        }

    }
}

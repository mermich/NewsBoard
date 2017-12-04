using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using ApiTools;
using NewBoardRestApi.FeedApi.Search;
using System.Linq.Expressions;
using System;

namespace NewBoardRestApi.ArticleApi
{
    /// <summary>
    /// plop
    /// </summary>
    [Route("api/[controller]")]
    public class ArticleApi : BaseAuthenticatedApi
    {
        public ArticleApi(NewsBoardContext newsBoardContext, SessionObject sessionObject) : base(newsBoardContext, sessionObject)
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
                .Where(subscriptionFilter(filter.SubscriptionFilter))
                .Where(a => !filter.HideReported || !a.Feed.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsReported))
                .Where(a => !filter.Feeds.Any() || filter.Feeds.Contains(a.FeedId))
                .Where(a => !filter.Tags.Any() ||a.Feed.FeedTags.Any(ft => filter.Tags.Contains(ft.TagId)))
                .OrderByDescending(a => a.PublishDate)
                .Take(filter.MaxItems)
                .ToArticleList(UserId);

            return result;
        }

        Expression<Func<Article, bool>> subscriptionFilter(SubscriptionFilter filter)
        {
            switch (filter)
            {
                case SubscriptionFilter.All:
                    return f => true;
                case SubscriptionFilter.OnlySubscribbed:
                    return f => f.Feed.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsSubscribed);
                case SubscriptionFilter.OnlyUnSubscribbed:
                    return f => !f.Feed.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsSubscribed);
                default:
                    return f => true;
            }
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

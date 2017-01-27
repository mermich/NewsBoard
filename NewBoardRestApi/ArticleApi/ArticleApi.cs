using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;

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
                .Include(a => a.Feed).ThenInclude(f=>f.UserFeeds)
                .Include(a => a.UserArticles)
                .FirstOrDefault(a => a.Id == articleId)
                .ToArticle(currentUser);
        }


        public ArticleVMList GetArticles(ArticleVMSearch filter = null)
        {
            if (filter == null)
                filter = new ArticleVMSearch();

            if (currentUser == null)
            {
                var result = NewsBoardContext.Articles
                    .Include(a => a.Feed)
                    .Include(a => a.UserArticles)
                    .Where(a => !filter.Feeds.Any() || filter.Feeds.Contains(a.FeedId))
                    .OrderBy(a => a.PublishDate)
                    .Take(filter.MaxItems)
                    .ToArticleList(currentUser);

                return result;
            }
            else
            {
                var result = NewsBoardContext.Articles
                    .Include(a => a.Feed)
                    .Include(a => a.UserArticles)
                    .Where(a => !filter.OnlyUserSubscription || a.Feed.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsSubscribed))
                    .Where(a => !filter.HideReported || !a.Feed.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsReported))
                    .Where(a => !filter.Feeds.Any() || filter.Feeds.Contains(a.FeedId))
                    .OrderBy(a => a.PublishDate)
                    .Take(filter.MaxItems)
                    .ToArticleList(currentUser);

                return result;
            }
        }


        [HttpGet("{id}")]
        [Route("OpenArticle")]
        public virtual ArticleVM OpenArticle(int id)
        {
            if (currentUser == null)
            {
                throw new BusinessLogicException("Seuls les utilisateurs authentifies peuvent ouvrir des articles.");
            }

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
            if (currentUser == null)
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
                userArticle = new UserArticle(currentUser, article);
                NewsBoardContext.UserArticles.Add(userArticle);
            }
            userArticle.IsHidden = true;
            NewsBoardContext.SaveChanges();
        }

    }
}

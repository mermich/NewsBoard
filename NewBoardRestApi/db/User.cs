using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.DataModel
{
    public class User
    {
		public virtual int Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string UserName { get; set; }
        

        public virtual List<UserArticle> UserArticles { get; set; }

        public virtual List<UserFeed> UserFeeds { get; set; }
        
        public virtual List<UserGroup> UserGroups { get; set; }


        public virtual List<Article> GetArticles()
        {
            var articles = UserFeeds
                .Where(uf => uf.IsSubscribed)
                .Select(uf => uf.Feed)
                .SelectMany(f=>f.Articles)
                .OrderByDescending(a => a.LastUpdatedTime)
                .ThenByDescending(a => a.PublishDate);

            var hiddenArticles = GetHiddenArticlesId();

            var v2 = articles.Where(a => !hiddenArticles.Contains(a.Id));


            return v2.Take(10).ToList();
        }

        

        public virtual List<int> GetHiddenArticlesId()
        {
            return UserArticles.Where(a => a.IsHidden).Select(a => a.Article.Id).ToList();
        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using ApiTools.Syndication;

namespace NewBoardRestApi.DataModel
{
    public class Article
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Summary { get; set; }

        public string Url { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        public Feed Feed { get; set; }

        public int FeedId { get; set; }

        public List<UserArticle> UserArticles { get; set; }

        public decimal Score
        {
            get
            {
                if (UserArticles != null && UserArticles.Any())
                    return (decimal)UserArticles.Average(ua => ua.Score);
                return 0;
            }
        }



        public Article()
        {
        }

        /// <summary>
        /// Fetch from database
        /// </summary>
        /// <param name="id"></param>
        public Article(int id)
        {
        }
    }

    internal static class ArticleExtentions
    {
        internal static Article ToArticle(this SyndicationItem item, Feed feed)
        {
            var article = new Article
            {
                Feed = feed,
                Label = item.Title,
                Summary = item.Content,
                Url = item.Url,
                PublishDate = item.PublishDate
            };

            return article;
        }

        internal static IEnumerable<Article> ToArticles(this IEnumerable<SyndicationItem> items, Feed feed)
        {
            return items.Select(i => i.ToArticle(feed));
        }
    }
}
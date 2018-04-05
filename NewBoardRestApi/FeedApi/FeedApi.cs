using ApiTools;
using ApiTools.HttpTools;
using ApiTools.SyndicationClient;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.FeedApi.Search;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NewBoardRestApi.FeedApi
{
    public class FeedApi : BaseAuthenticatedApi
    {
        public FeedApi(NewsBoardContext newsBoardContext, SessionObject sessionObject) : base(newsBoardContext, sessionObject)
        {
        }

        public virtual void RefreshFeedArticles(int feedId)
        {
            var feed = NewsBoardContext.Feeds.FirstOrDefault(f => f.Id == feedId);
            var xdoc = new XDocumentPageWrapper(feed.SyndicationUri, new HttpClientWrapper(feed.SyndicationUri).FetchResponse());
            var feedDetails = new SyndicationClientStrategy(xdoc).GetSyndicationClient().GetSyndicationContent();

            feed.LastTimeFetched = DateTime.Now;

            // Not ovveriding Description or Title
            //feed.Description = feedDetails.Description;
            //feed.Title = feedDetails.Title;

            // Merge the list of articles.
            var existingArticles = NewsBoardContext.Articles.Where(a => a.FeedId == feedId);
            foreach (var article in feedDetails.Items.ToArticles(feed))
            {
                var existingArticle = existingArticles.FirstOrDefault(a => a.Url == article.Url);
                if (existingArticle != null)
                {
                    // Article exists.
                    existingArticle.Label = article.Label;
                    existingArticle.LastUpdatedTime = article.LastUpdatedTime;
                    existingArticle.PublishDate = article.PublishDate;
                    existingArticle.Summary = article.Summary;
                }
                else
                {
                    NewsBoardContext.Articles.Add(article);
                }
            }


            if (UserId == UnAuthenticatedUserId)
            {
                // nothing more to do;
            }
            else
            {
                var existingUserFeed = NewsBoardContext.UserFeeds.Include(uf => uf.User).FirstOrDefault(uf => uf.User.Id == UserId && uf.Feed.Id == feedId);

                if (existingUserFeed != null)
                {
                    existingUserFeed.Subscribe();
                }
                else
                {
                    var newSubscription = new UserFeed(UserId, feed);
                    NewsBoardContext.UserFeeds.Add(newSubscription);
                }
            }
            NewsBoardContext.SaveChanges();
        }



        public void RefreshFeedsArticles()
        {
            Exception e = null;
            var feeds = NewsBoardContext.Feeds.Include(f => f.Articles).ToList();

            foreach (var feed in feeds)
            {
                try
                {
                    RefreshFeedArticles(feed.Id);
                }
                catch (Exception ex)
                {
                    e = ex;
                }

            }

            if (e != null)
            {
                throw e;
            }
        }

        public void RefreshFeedInformations()
        {
            var feeds = NewsBoardContext.Feeds
                .Include(f => f.WebSite)
                .ToList();

            foreach (var feed in feeds)
            {
                var details = new LookupWebSiteApi().GetWebSiteDetails(feed.WebSite.Uri);
                feed.WebSite.IconUrl = details.IconUri.AbsolutePath;
                NewsBoardContext.SaveChanges();
            }
        }


        public Feed CreateSubscription(Uri websiteUri, Uri syndicationUri)
        {
            var xdoc = new XDocumentPageWrapper(syndicationUri, new HttpClientWrapper(syndicationUri).FetchResponse());
            var feedDetails = new SyndicationClientStrategy(xdoc).GetSyndicationClient().GetSyndicationContent();


            var websiteDetails = new LookupWebSiteApi().GetWebSiteDetails(websiteUri);
            var website = NewsBoardContext.WebSites.FirstOrDefault(w => w.Uri == websiteDetails.Uri);
            if (website == null)
            {
                website = new WebSite
                {
                    Title = websiteDetails.Title,
                    Url = websiteDetails.Uri.AbsoluteUri,
                    IconUrl = websiteDetails.IconUri.AbsoluteUri
                };
            }

            var feed = new Feed()
            {
                SyndicationUrl = syndicationUri.AbsoluteUri,
                LastTimeFetched = DateTime.Now,
                Description = feedDetails.Description,
                Title = feedDetails.Title,
                WebSite = website
            };
            website.Feeds.Add(feed);

            NewsBoardContext.Feeds.Add(feed);
            NewsBoardContext.SaveChanges();

            RefreshFeedArticles(feed.Id);

            return feed;
        }

        public FeedVMList ListFeed(FeedVMSearch filter)
        {
            // Filter
            if (filter == null)
                filter = new FeedVMSearch();

            return NewsBoardContext
                .Feeds
                .Take(filter.MaxItems)
                .Where(f => !filter.Tags.Any() || f.FeedTags.Any(ft => filter.Tags.Contains(ft.TagId)))
                .Where(SubscriptionFilter(filter.SubscriptionFilter))
                //.Where(f => filter.HideReported || !f.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsReported))
                .Include(f => f.UserFeeds)
                .ThenInclude(uf => uf.User)
                .Include(f => f.Articles)
                .ThenInclude(a => a.UserArticles)
                .Include(f => f.WebSite)
                .OrderBy(PickRadomItemsFilter(filter))
                .ToList()
                .OrderBy(OrderBy(filter.OrderBy))
                .ToFeedVMList(UserId);
        }

        Expression<Func<Feed, string>> PickRadomItemsFilter(FeedVMSearch by)
        {
            if (by.ShouldPickRandomItems)
            {
                return f => new Guid().ToString();
            }
            else
            {
                return OrderByExpr(by.OrderBy);
            }
        }


        Expression<Func<Feed, string>> OrderByExpr(FeedListOrderBy by)
        {
            switch (by)
            {
                case FeedListOrderBy.Name:
                    return f => f.Title;
                case FeedListOrderBy.Subscriptions:
                    return f => f.Title;
                default:
                    return f => f.Title;
            }
        }

        Func<Feed, string> OrderBy(FeedListOrderBy by)
        {
            switch (by)
            {
                case FeedListOrderBy.Name:
                    return f => f.Title;
                case FeedListOrderBy.Subscriptions:
                    return f => f.Title;
                default:
                    return f => f.Title;
            }
        }

        Expression<Func<Feed, bool>> SubscriptionFilter(SubscriptionFilter filter)
        {

            switch (filter)
            {
                case Search.SubscriptionFilter.All:
                    return f => true;
                case Search.SubscriptionFilter.OnlySubscribbed:
                    if (UserId == UnAuthenticatedUserId)
                        return f => false;
                    else
                        return f => f.UserFeeds.Any(uf => uf.UserId == UserId && uf.IsSubscribed);
                case Search.SubscriptionFilter.OnlyUnSubscribbed:
                    if (UserId == UnAuthenticatedUserId)
                        return f => false;
                    else
                        return f => f.UserFeeds.Any(uf => uf.UserId != UserId || !uf.IsSubscribed);
                default:
                    return f => true;
            }
        }

        public Feed CreateSubscriptionAndSubScribe(Uri websiteUri, Uri feedUri)
        {
            ThrowExIfUnAuthenticated();

            if (DoesFeedExists(feedUri))
            {
                // Feed exists.
            }
            else
            {
                CreateSubscription(websiteUri, feedUri);
            }

            var feed = GetFeed(feedUri);
            SubscribeFeed(feed.Id);

            return feed;
        }

        public virtual bool DoesFeedExists(Uri feedUri)
        {
            return NewsBoardContext.Feeds.Any(f => f.SyndicationUrl == feedUri.AbsoluteUri);
        }


        private Feed GetFeed(Uri feedUri)
        {
            return NewsBoardContext.Feeds.FirstOrDefault(f => f.SyndicationUrl == feedUri.AbsoluteUri);
        }

        public virtual void SubscribeFeed(int feedId)
        {
            ThrowExIfUnAuthenticated();

            var existingUserFeed = NewsBoardContext.UserFeeds
                .Include(uf => uf.Feed)
                .Include(uf => uf.User)
                .FirstOrDefault(uf => uf.UserId == UserId && uf.FeedId == feedId);

            if (existingUserFeed != null)
            {
                existingUserFeed.Subscribe();
            }
            else
            {
                var feed = NewsBoardContext.Feeds.FirstOrDefault(f => f.Id == feedId);
                var newSubscription = new UserFeed(UserId, feed);
                NewsBoardContext.UserFeeds.Add(newSubscription);
            }

            NewsBoardContext.SaveChanges();
        }


        public virtual void UnSubscribeFeed(int feedId)
        {
            ThrowExIfUnAuthenticated();

            var existingUserFeed = NewsBoardContext.UserFeeds.FirstOrDefault(uf => uf.UserId == UserId && uf.FeedId == feedId);
            if (existingUserFeed != null)
            {
                existingUserFeed.UnSubscribe();
            }
            else
            {
                //nothing to do.
            }

            NewsBoardContext.SaveChanges();
        }

        private UserFeed GetOrCreateUserFeed(int feedId)
        {
            var userFeed = NewsBoardContext.UserFeeds.FirstOrDefault(uf => uf.UserId == UserId && uf.FeedId == feedId);
            if (userFeed == null)
            {
                var feed = NewsBoardContext.Feeds.FirstOrDefault(f => f.Id == feedId);
                userFeed = new UserFeed(UserId, feed);
                NewsBoardContext.UserFeeds.Add(userFeed);
            }
            return userFeed;
        }

        public virtual void OpenFeed(int feedId)
        {
            var userFeed = GetOrCreateUserFeed(feedId);
            userFeed.Open();
            NewsBoardContext.SaveChanges();
        }

        public virtual void ReportFeed(int feedId)
        {
            ThrowExIfUnAuthenticated();
            var userFeed = GetOrCreateUserFeed(feedId);
            userFeed.Report();
            NewsBoardContext.SaveChanges();
        }

        public virtual void StopDisplayFeed(int feedId)
        {
            ThrowExIfUnAuthenticated();
            var userFeed = GetOrCreateUserFeed(feedId);
            userFeed.StopDisplay();
            NewsBoardContext.SaveChanges();
        }


        public virtual FeedVM GetFeed(int feedId)
        {
            return NewsBoardContext
                .Feeds
                .Include(f => f.WebSite)
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles).ThenInclude(article => article.UserArticles)
                .FirstOrDefault(f => f.Id == feedId)
                .ToFeedVM(UserId);
        }

        public virtual ArticleVMList GetArticles(int feedId)
        {
            return NewsBoardContext
                .Articles
                .Include(a => a.UserArticles)
                .Where(a => a.FeedId == feedId)
                .ToArticleList(UserId);
        }

        public void SaveFeed(FeedEditVM feed)
        {
            ThrowExIfUnAuthenticated();

            var feedDb = NewsBoardContext.Feeds
                .Include(f => f.FeedTags)
                .Include(f => f.WebSite)
                .FirstOrDefault(f => f.Id == feed.Id);

            feedDb.Description = feed.Description;
            feedDb.SyndicationUrl = feed.SyndicationUrl;
            feedDb.Title = feed.Title;
            feedDb.WebSite.IconUrl = feed.IconUrl;
            feed.WebSiteUrl = feed.WebSiteUrl;

            // Merge tags
            foreach (var item in feed.Tags.Items)
            {
                var existingTag = feedDb.FeedTags.FirstOrDefault(ft => ft.TagId == item.Value);
                if (existingTag == null)
                {
                    if (item.IsSelected)
                    {
                        var tagToattach = NewsBoardContext.Tags.FirstOrDefault(t => t.Id == item.Value);
                        feedDb.FeedTags.Add(new FeedTag { Feed = feedDb, Tag = tagToattach });
                    }
                }
                else
                {
                    // Remove  tag
                    if (!item.IsSelected)
                    {
                        NewsBoardContext.FeedTags.Remove(existingTag);
                    }
                }
            }
            NewsBoardContext.SaveChanges();
        }

        public virtual FeedEditVM GetFeedEdit(int feedId)
        {
            var possibleTags = NewsBoardContext.Tags.ToList();

            var result = NewsBoardContext.Feeds
                .Include(f => f.WebSite)
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles).ThenInclude(article => article.UserArticles)
                .Include(f => f.FeedTags)
                .FirstOrDefault(f => f.Id == feedId)
                .ToFeedEdit(possibleTags, UserId);

            return result;
        }
    }
}

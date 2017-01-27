using System;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using SiteParser;
using NewBoardRestApi.ArticleApi;

namespace NewBoardRestApi.FeedApi
{
    public class FeedApi : BaseAuthenticatedApi
    {
        public FeedApi(int userId) : base(userId)
        {
        }

        public FeedApi(User user) : base(user)
        {
        }

        public FeedVMPreview GetPreview(string url)
        {
            var syndicationUrl = new HttpClientWrapper(url)
                .GetResponse()
                .ToPageSyndicationFinder()
                .GetSyndicationURl(url);

            var preview = new HttpClientWrapper(syndicationUrl)
                .GetResponse()
                .ToXDocument()
                .ToFeedClientStrategy(syndicationUrl)
                .FeedClient()
                .ToFeedPreview();

            return preview;
        }


        public virtual void Refresh(int feedId)
        {
            var feed = NewsBoardContext.Feeds.FirstOrDefault(f => f.Id == feedId);

            var feedClient = new HttpClientWrapper(feed.SyndicationUrl)
                .GetResponse()
                .ToXDocument()
                .ToFeedClientStrategy(feed.SyndicationUrl)
                .FeedClient();

            feed.LastTimeFetched = DateTime.Now;
            feed.Description = feedClient.SyndicationSummary().Description;
            feed.Title = feedClient.SyndicationSummary().Title;

            NewsBoardContext.Articles.AddRange(feedClient.Items().ToArticles(feed));

            var existingUserFeed = NewsBoardContext.UserFeeds
                .Include(uf => uf.User)
                .FirstOrDefault(uf => uf.User.Id == currentUser.Id && uf.Feed.Id == feedId);

            if (existingUserFeed != null)
            {
                existingUserFeed.Subscribe();
            }
            else
            {
                var newSubscription = new UserFeed(currentUser, existingUserFeed.Feed);
                NewsBoardContext.UserFeeds.Add(newSubscription);
            }

            NewsBoardContext.SaveChanges();
        }


        public Feed CreateSubscription(string syndicationUrl)
        {
            var feedClient = new HttpClientWrapper(syndicationUrl)
                .GetResponse()
                .ToXDocument()
                .ToFeedClientStrategy(syndicationUrl)
                .FeedClient();

            var feed = new Feed()
            {
                SyndicationUrl = syndicationUrl,
                LastTimeFetched = DateTime.Now,
                Description = feedClient.SyndicationSummary().Description,
                Title = feedClient.SyndicationSummary().Title
                //,
                //WebSite = new WebSite
                //{
                //    Url = feedClient.SyndicationSummary().WebSiteUrl,
                //    Description = feedClient.SyndicationSummary().Description,
                //    Title = feedClient.SyndicationSummary().Title
                //}
            };

            NewsBoardContext.Feeds.Add(feed);
            NewsBoardContext.SaveChanges();

            return feed;
        }

        public FeedVMList ListFeed(FeedVMSearch filter)
        {
            // Filter
            if (filter == null)
                filter = new FeedVMSearch();

            if (currentUser == null)
            {
                return NewsBoardContext
                    .Feeds
                    .Take(filter.MaxItems)
                    .Where(f => !filter.Tags.Any() || f.FeedTags.Any(ft => filter.Tags.Contains(ft.TagId)))
                    .Include(f => f.UserFeeds)
                    .ThenInclude(uf => uf.User)
                    .Include(f => f.Articles)
                    .ThenInclude(a => a.UserArticles)
                    .OrderBy(f => f.Title)
                    .Take(10)
                    .ToFeedVMList(currentUser);
            }
            else
            {
                return NewsBoardContext
                    .Feeds
                    .Take(filter.MaxItems)
                    .Where(f => !filter.Tags.Any() || f.FeedTags.Any(ft => filter.Tags.Contains(ft.TagId)))
                    .Where(f => !filter.OnlyUserSubscription || f.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsSubscribed))
                    .Where(f => filter.HideReported || !f.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsReported))
                    .Include(f => f.UserFeeds)
                    .ThenInclude(uf => uf.User)
                    .Include(f => f.Articles)
                    .ThenInclude(a => a.UserArticles)
                    .OrderBy(f => f.Title)
                    .ToFeedVMList(currentUser);
            }
        }


        public Feed CreateSubscriptionAndSubScribe(string addFeedUrl)
        {
            if (currentUser == null)
            {
                throw new NeedAuthenticationException();
            }

            if (NewsBoardContext.Feeds.Any(f => f.SyndicationUrl == addFeedUrl))
            {
                throw new BusinessLogicException("Le flux existe deja.");
            }
            else
            {
                var feed = CreateSubscription(addFeedUrl);
                SubscribeFeed(feed.Id);

                return feed;
            }
        }


        public virtual void SubscribeFeed(int feedId)
        {
            if (currentUser == null)
            {
                throw new NeedAuthenticationException();
            }

            var existingUserFeed = NewsBoardContext.UserFeeds
                .Include(uf => uf.Feed)
                .Include(uf => uf.User)
                .FirstOrDefault(uf => uf.UserId == currentUser.Id && uf.FeedId == feedId);

            if (existingUserFeed != null)
            {
                existingUserFeed.Subscribe();
            }
            else
            {
                var feed = NewsBoardContext.Feeds.FirstOrDefault(f => f.Id == feedId);
                var newSubscription = new UserFeed(currentUser, feed);
                NewsBoardContext.UserFeeds.Add(newSubscription);
            }

            NewsBoardContext.SaveChanges();
        }


        public virtual void UnSubscribeFeed(int feedId)
        {
            if (currentUser == null)
            {
                throw new BusinessLogicException("Seuls les utilisateurs authentifies peuvent se desabonner.");
            }

            var existingUserFeed = NewsBoardContext.UserFeeds.FirstOrDefault(uf => uf.UserId == currentUser.Id && uf.FeedId == feedId);
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



        public virtual void OpenFeed(int feedId)
        {
            var existingUserFeed = NewsBoardContext.UserFeeds.FirstOrDefault(uf => uf.UserId == currentUser.Id && uf.FeedId == feedId);
            if (existingUserFeed != null)
            {
                existingUserFeed.Open();
            }
            else
            {
                //nothing to do.
            }

            NewsBoardContext.SaveChanges();
        }

        public virtual void ReportFeed(int feedId)
        {
            if (currentUser == null)
            {
                throw new NeedAuthenticationException();
            }

            var existingUserFeed = NewsBoardContext.UserFeeds.FirstOrDefault(uf => uf.UserId == currentUser.Id && uf.FeedId == feedId);
            if (existingUserFeed != null)
            {
                existingUserFeed.Report();
            }
            else
            {
                existingUserFeed = new UserFeed(currentUser.Id, feedId);
            }
            NewsBoardContext.SaveChanges();
        }

        public virtual FeedVM GetFeed(int feedId)
        {
            return NewsBoardContext.Feeds
                //.Include(f => f.WebSite)
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles).ThenInclude(article => article.UserArticles)
                .FirstOrDefault(f => f.Id == feedId)
                .ToFeedVM(currentUser);
        }

        public virtual ArticleVMList GetArticles(int feedId)
        {
            return NewsBoardContext.Articles
                .Include(a => a.UserArticles)
                .Where(a => a.FeedId == feedId)
                .ToArticleList(currentUser);
        }

        public void SaveFeed(FeedEditVM feed)
        {
            if (currentUser == null)
            {
                throw new NeedAuthenticationException();
            }

            var feedDb = NewsBoardContext.Feeds
                .Include(f => f.FeedTags)
                .FirstOrDefault(f => f.Id == feed.Id);

            feedDb.Description = feed.Description;
            feedDb.SyndicationUrl = feed.SyndicationUrl;
            feedDb.Title = feed.Title;
            feed.WebSiteUrl = feed.WebSiteUrl;

            //merge tags
            foreach (var item in feed.Tags.Items)
            {
                var existingTag = feedDb.FeedTags.FirstOrDefault(ft => ft.TagId == item.Id);
                if (existingTag == null)
                {
                    if (item.IsSelected)
                    {
                        var tagToattach = NewsBoardContext.Tags.FirstOrDefault(t => t.Id == item.Id);
                        feedDb.FeedTags.Add(new FeedTag { Feed = feedDb, Tag = tagToattach });
                    }
                }
                else
                {
                    //remove  tag
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
                //.Include(f => f.WebSite)
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles).ThenInclude(article => article.UserArticles)
                .Include(f => f.FeedTags)
                .FirstOrDefault(f => f.Id == feedId)
                .ToFeedEdit(possibleTags, currentUser);

            return result;
        }
    }
}

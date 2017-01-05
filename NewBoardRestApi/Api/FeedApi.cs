using System;
using NewBoardRestApi.DataModel;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.Http;
using NewBoardRestApi.Api.Model;

namespace NewBoardRestApi.Api
{
    public class FeedApi : BaseAuthenticatedApi
    {
        public FeedApi(string userToken) : base(userToken)
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
                Title = feedClient.SyndicationSummary().Title,
                WebSiteUrl = feedClient.SyndicationSummary().WebSiteUrl,

            };

            NewsBoardContext.Feeds.Add(feed);
            NewsBoardContext.SaveChanges();

            return feed;
        }

        public FeedVMList ListFeed(FeedListFilterVM filter)
        {
            // Filter
            if (filter == null)
                filter = new FeedListFilterVM();

            // Initialize query.
            return NewsBoardContext
                .Feeds
                .Take(filter.MaxItems)
                .Where(f => !filter.Tags.Any() || f.FeedTags.Any(ft => filter.Tags.Contains(ft.TagId)))
                .Where(f => filter.OnlyUserSubscription || f.UserFeeds.Any(uf => uf.UserId == currentUser.Id))
                .Where(f => filter.HideReported || !f.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsReported))
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles)
                .OrderBy(f => f.Title)
                .ToFeedVMList();
        }


        public Feed CreateSubscriptionAndSubScribe(string addFeedUrl)
        {
            if (NewsBoardContext.Feeds.Any(f => f.SyndicationUrl == addFeedUrl))
            {

            }


            var feed = CreateSubscription(addFeedUrl);
            SubscribeFeed(feed.Id);

            return feed;
        }


        public virtual void SubscribeFeed(int feedId)
        {
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
                .Include(f => f.UserFeeds)
                .Include(f => f.Articles)
                .FirstOrDefault(f => f.Id == feedId)
                .ToFeedVM();
        }

        public virtual ArticleVMList GetArticles(int feedId)
        {
            return NewsBoardContext.Articles
                .Where(a => a.FeedId == feedId)
                .ToArticleList();
        }

        public void SaveFeed(FeedEditVM feed)
        {
            var feedDb = NewsBoardContext.Feeds
                .Include(f=>f.FeedTags)
                .FirstOrDefault(f => f.Id == feed.Id);

            feedDb.Description = feed.Description;
            feedDb.SyndicationUrl = feed.SyndicationUrl;
            feedDb.Title = feed.Title;
            feed.WebSiteUrl = feed.WebSiteUrl;

            //merge tags
            foreach (var item in feed.Tags.Items)
            {
                var existingTag = feedDb.FeedTags.FirstOrDefault(ft => ft.TagId == item.Id);
                if(existingTag == null)
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

            var feed = NewsBoardContext.Feeds
               .Include(f => f.UserFeeds)
               .Include(f => f.Articles)
               .Include(f => f.FeedTags)
               .FirstOrDefault(f => f.Id == feedId);

            var possibleTags = NewsBoardContext.Tags.ToList();

            var result = feed.ToFeedEdit(possibleTags);

            return result;
        }
    }
}

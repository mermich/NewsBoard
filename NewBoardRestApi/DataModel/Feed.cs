using System;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.DataModel
{
    public class Feed
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string WebSiteUrl { get; set; }

        public string SyndicationUrl { get; set; }

        public DateTime LastTimeFetched { get; set; }

        public bool IsActive { get; set; }
        
        public List<FeedTag> FeedTags { get; set; }

        public List<UserFeed> UserFeeds { get; set; }

        public List<Article> Articles { get; set; }
        
        public int Subscribers
        {
            get
            {
                if (UserFeeds != null)
                    return UserFeeds.Where(uf => uf.IsSubscribed).Count();
                else
                    return 0;
            }
        }

        public Feed()
        {
        }
    }
}
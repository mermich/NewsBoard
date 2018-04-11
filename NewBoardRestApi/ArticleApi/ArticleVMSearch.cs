using NewBoardRestApi.FeedApi.Search;
using System.Collections.Generic;

namespace NewBoardRestApi.ArticleApi
{
    public class ArticleVMSearch
    {
        public List<int> Feeds { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 100;

        public SubscriptionFilter SubscriptionFilter { get; set; } = SubscriptionFilter.All;

        public bool HideReported { get; set; } = true;

        public string OrderBy { get; set; }

        public List<int> Tags { get; set; } = new List<int>();

        public static ArticleVMSearch BuildSerachByFeedId(int feedId)
        {
            return new ArticleVMSearch
            {
                Feeds = new List<int> { feedId },
                HideReported = false
            };
        }
    }
}

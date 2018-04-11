using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.FeedApi
{
    public class FeedActionVM
    {
        public int Id { get; set; }

        public bool IsSubscribed { get; set; }


        public FeedActionVM() { }

        public FeedActionVM(Feed feed, IEnumerable<int> userFeeds)
        {
            Id = feed.Id;
            IsSubscribed = userFeeds.Contains(feed.Id);
        }
    }
}

using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{

    public class FeedVMList
    {
        public List<FeedVM> Feeds { get; set; }

        public FeedVMList() { }

        public FeedVMList(IEnumerable<FeedVM> feeds)
        {
            Feeds = feeds.ToList();
        }
    }

    public static class FeedVMListExtentions
    {
        public static FeedVMList ToFeedVMList(this IEnumerable<Feed> items, User currentUser)
        {
            return new FeedVMList(items.Select(i => i.ToFeedVM(currentUser)));
        }
    }
}

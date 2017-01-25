using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{

    public class FeedVMList
    {
        public FeedVMListOptions Options { get; set; } = new FeedVMListOptions();

        public List<FeedVM> Feeds { get; set; } = new List<FeedVM>();

        public FeedVMList()
        {

        }

        public FeedVMList(IEnumerable<FeedVM> feeds)
        {
            Feeds = feeds.ToList();
        }
    }

    internal static class FeedVMListExtentions
    {
        internal static FeedVMList ToFeedVMList(this IEnumerable<Feed> items, User currentUser)
        {
            return new FeedVMList(items.Select(i => i.ToFeedVM(currentUser)));
        }
    }
}

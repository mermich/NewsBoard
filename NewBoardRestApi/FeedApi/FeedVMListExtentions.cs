using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedVMListExtentions
    {
        internal static FeedVMList ToFeedVMList(this IEnumerable<Feed> items, int userId)
        {
            return new FeedVMList(items.Select(i => i.ToFeedVM(userId)));
        }
    }
}

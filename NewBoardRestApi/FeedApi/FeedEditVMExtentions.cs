using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedEditVMExtentions
    {
        internal static FeedEditVM ToFeedEdit(this Feed feed, IEnumerable<Tag> possibleTags, int userId)
        {
            return new FeedEditVM(feed, possibleTags, userId);
        }
    }
}

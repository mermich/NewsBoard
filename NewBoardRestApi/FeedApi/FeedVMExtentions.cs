using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedVMExtentions
    {
        internal static FeedVM ToFeedVM(this Feed feed, int userId)
        {
            return new FeedVM(feed, userId);
        }
    }
}

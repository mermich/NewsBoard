using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedVMExtentions
    {
        internal static FeedVM ToFeedVM(this Feed feed, User currentUser)
        {
            return new FeedVM(feed, currentUser);
        }
    }
}

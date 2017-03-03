using SiteParser.Client;

namespace NewBoardRestApi.DiscoverApi
{
    internal static class DiscoverFeedVDiscoverFeedVMExtentionsMExtentions
    {
        internal static DiscoverFeedVM ToDiscoverFeedVM(this AFeedClient feedClient)
        {
            return new DiscoverFeedVM(feedClient);
        }
    }
}

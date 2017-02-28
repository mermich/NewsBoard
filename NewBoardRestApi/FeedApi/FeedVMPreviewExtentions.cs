using SiteParser.Client;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedVMPreviewExtentions
    {
        internal static FeedVMPreview ToFeedPreview(this AFeedClient feedClient)
        {
            return new FeedVMPreview(feedClient);
        }
    }
}

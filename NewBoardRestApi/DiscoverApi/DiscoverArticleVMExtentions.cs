using SiteParser;

namespace NewBoardRestApi.DiscoverApi
{
    internal static class DiscoverArticleVMExtentions
    {
        internal static DiscoverArticleVM ToArticlePreview(this SyndicationItem item)
        {
            return new DiscoverArticleVM(item);
        }
    }
}
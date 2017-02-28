using SiteParser;

namespace NewBoardRestApi.ArticleApi
{
    internal static class ArticleVMPreviewExtentions
    {
        internal static ArticleVMPreview ToArticlePreview(this SyndicationItem item)
        {
            return new ArticleVMPreview(item);
        }
    }
}
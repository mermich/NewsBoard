using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.ArticleApi
{
    internal static class ArticleVMExtentions
    {
        internal static ArticleVM ToArticle(this Article item, int userId)
        {
            return new ArticleVM(item, userId);
        }
    }
}

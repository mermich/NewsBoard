using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.ArticleApi
{
    internal static class ArticleVMExtentions
    {
        internal static ArticleVM ToArticle(this Article item, User currentUser)
        {
            return new ArticleVM(item, currentUser);
        }
    }
}

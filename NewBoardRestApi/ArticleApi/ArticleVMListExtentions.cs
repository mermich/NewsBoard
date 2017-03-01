using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.ArticleApi
{
    internal static class ArticleVMListExtentions
    {
        internal static ArticleVMList ToArticleList(this IEnumerable<DataModel.Article> items, User currentUser)
        {
            return new ArticleVMList(items.Select(i => i.ToArticle(currentUser)));
        }
    }
}

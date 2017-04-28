using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.ArticleApi
{
    internal static class ArticleVMListExtentions
    {
        internal static ArticleVMList ToArticleList(this IEnumerable<DataModel.Article> items, int userId)
        {
            return new ArticleVMList(items.Select(i => i.ToArticle(userId)));
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleVMList
    {
        public List<ArticleVM> Articles { get; set; }

        public ArticleVMList() { }

        public ArticleVMList(IEnumerable<ArticleVM> articles)
        {
            Articles = articles.ToList();
        }
    }

    public static class ArticleVMListExtentions
    {
        public static ArticleVMList ToArticleList(this IEnumerable<DataModel.Article> items)
        {
            return new ArticleVMList(items.Select(i => i.ToArticle()));
        }
    }
}

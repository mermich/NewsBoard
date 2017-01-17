using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleVMList
    {
        public string Title { get; set; }

        public List<ArticleVM> Articles { get; set; }

        public ArticleVMList() { }

        public ArticleVMList(IEnumerable<ArticleVM> articles)
        {
            Articles = articles.ToList();
        }
    }

    public static class ArticleVMListExtentions
    {
        public static ArticleVMList ToArticleList(this IEnumerable<DataModel.Article> items, User currentUser)
        {
            return new ArticleVMList(items.Select(i => i.ToArticle(currentUser)));
        }
    }
}

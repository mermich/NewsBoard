using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.ArticleApi
{
    public class ArticleVMList
    {
        public ArticleVMListOptions Options { get; set; } = new ArticleVMListOptions();

        public List<ArticleVM> Articles { get; set; } = new List<ArticleVM>();

        public ArticleVMList() { }

        public ArticleVMList(IEnumerable<ArticleVM> articles)
        {
            Articles = articles.ToList();
        }
    }
}

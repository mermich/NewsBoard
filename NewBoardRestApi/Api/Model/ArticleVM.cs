using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleVM
    {
        public int Id { get; private set; }

        public bool IsNew { get; set; }

        public string IsNewClass
        {
            get
            {
                if (IsNew)
                    return "IsNewClass";
                return "";
            }
        }

        public string Label { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }

        public int Subscribers { get; private set; }

        public decimal Score { get; private set; }

        public int FeedId { get; private set; }


        public ArticleVM()
        {
        }

        public ArticleVM(Article article, User currentUser)
        {
            Id = article.Id;
            Label = article.Label;
            Description = article.Summary;
            Url = article.Url;
            Score = article.Score;
            FeedId = article.FeedId;

            if(currentUser != null)
                IsNew = article.UserArticles == null || !article.UserArticles.Any(ua => ua.UserId == currentUser.Id);
        }
    }

    public static class ArticleVMExtentions
    {
        public static ArticleVM ToArticle(this Article item, User currentUser)
        {
            return new ArticleVM(item, currentUser);
        }
    }
}

using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleVM
    {
        public int Id { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }

        public int Subscribers { get; private set; }

        public decimal Score { get; private set; }

        public int FeedId { get; private set; }


        public ArticleVM()
        {
        }

        public ArticleVM(Article article)
        {
            Id = article.Id;
            Label = article.Label;
            Description = article.Summary;
            Url = article.Url;
            Score = article.Score;
            FeedId = article.FeedId;
        }
    }

    public static class ArticleVMExtentions
    {
        public static ArticleVM ToArticle(this Article item)
        {
            return new ArticleVM(item);
        }
    }
}

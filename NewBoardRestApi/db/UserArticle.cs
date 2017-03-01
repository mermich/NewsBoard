namespace NewBoardRestApi.DataModel
{
    public class UserArticle
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public bool IsOpened { get; set; }

        //public DateTime OpenedTime { get; set; } = DateTime.Now;

        public bool IsHidden { get; set; }

        public int Score { get; set; } = 2;

        public UserArticle()
        {
        }

        public UserArticle(User user, Article article)
        {
            Article = article;
            User = user;
        }
    }
}
namespace NewBoardRestApi.DataModel
{
    public class UserFeed
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public Feed Feed { get; set; }

        public int FeedId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public bool IsSubscribed { get; set; } = true;

        public bool IsReported { get; set; }

        public UserFeed()
        {
        }


        public UserFeed(User user, Feed feed)
        {
            User = user;
            Feed = feed;
        }

        public UserFeed(int userId, int feedId)
        {
            UserId = userId;
            FeedId = feedId;
        }

        public virtual void Subscribe()
        {
            IsSubscribed = true;
        }

        public virtual void UnSubscribe()
        {
            IsSubscribed = false;
        }

        public virtual void Open()
        {
        }

        public virtual void Report()
        {
            IsReported = true;
        }
    }
}
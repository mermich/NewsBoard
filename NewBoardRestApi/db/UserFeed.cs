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

        public bool IsSubscribed { get; set; }

        public bool IsReported { get; set; }

        public bool IsHidden { get; set; }

        public bool IsOpened { get; set; }

        public UserFeed()
        {
        }


        public UserFeed(int userId, Feed feed)
        {
            UserId = userId;
            Feed = feed;
            IsSubscribed = true;
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
            IsOpened = true;
        }

        public virtual void Report()
        {
            IsReported = true;
            StopDisplay();
           
        }

        public virtual void StopDisplay()
        {
            IsHidden = true;
            UnSubscribe();
        }
    }
}
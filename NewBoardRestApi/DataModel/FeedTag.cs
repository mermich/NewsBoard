namespace NewBoardRestApi.DataModel
{
    public class FeedTag
    {
        public int Id { get; set; }
        

        public Feed Feed { get; set; }

        public int FeedId { get; set; }

        public Tag Tag { get; set; }

        public int TagId { get; set; }


        public FeedTag()
        {
        }

        public FeedTag(Feed feed, Tag tag)
        {
            Feed = feed;
            Tag = tag;
        }

        public FeedTag(int feedId, int tagId)
        {
            FeedId = feedId;
            TagId = tagId;
        }

       
    }
}
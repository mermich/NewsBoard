using System.Collections.Generic;

namespace NewBoardRestApi.DataModel
{
    public class Tag
    {
        public virtual int Id { get; set; }

        public virtual string Label { get; set; }

        public bool Enabled { get; set; }

        public List<FeedTag> FeedTags { get; set; }

        public Tag()
        {
            Enabled = true;
        }
    }
}
using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.TagApi
{
    public class TagVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public int HitCount { get; set; }


        public TagVM()
        {
        }

        public TagVM(Tag tag, List< FeedTag> allTags)
        {
            Id = tag.Id;
            Label = tag.Label;
            HitCount = tag.FeedTags.Count;
        }
    }
}

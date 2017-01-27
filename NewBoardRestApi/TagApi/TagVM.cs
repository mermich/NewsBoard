using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

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

    internal static class TagVMExtentions
    {
        internal static TagVM ToTag(this Tag item, List<FeedTag> allTags)
        {
            return new TagVM(item, allTags);
        }

        internal static List<TagVM> ToTags(this List<Tag> items, List<FeedTag> allTags)
        {
            return items.Select(i => i.ToTag(allTags)).ToList();
        }
    }
}

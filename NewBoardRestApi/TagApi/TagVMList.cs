using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.TagApi
{
    public class TagVMList
    {
        public List<TagVM> Tags { get; set; }

        public TagVMList() { }

        public TagVMList(IEnumerable<TagVM> tags)
        {
            Tags = tags.ToList();
        }
    }


    internal static class TagVMListExtentions
    {
        internal static TagVMList ToTagVMList(this IEnumerable<Tag> items, List<FeedTag> allTags)
        {
            return new TagVMList(items.Select(i => i.ToTag(allTags)));
        }
    }
}

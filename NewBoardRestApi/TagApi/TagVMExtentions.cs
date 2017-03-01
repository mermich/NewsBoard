using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.TagApi
{
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

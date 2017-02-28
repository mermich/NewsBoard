using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.TagApi
{
    internal static class TagVMListExtentions
    {
        internal static TagVMList ToTagVMList(this IEnumerable<Tag> items, List<FeedTag> allTags)
        {
            return new TagVMList(items.Select(i => i.ToTag(allTags)));
        }
    }
}

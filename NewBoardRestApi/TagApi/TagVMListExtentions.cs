using NewBoardRestApi.DataModel;
using Selectable;
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

        internal static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Tag> items, List<FeedTag> selected)
        {
            return new SelectableItemList<int>(items.Select(i=>i.ToSelectableItem(selected)));
        }
    }
}

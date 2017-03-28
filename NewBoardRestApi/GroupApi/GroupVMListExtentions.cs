using ApiTools.Selectable;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.GroupApi
{
    internal static class GroupVMListExtentions
    {

        internal static GroupVMList ToGroupVMList(this IEnumerable<Group> groups)
        {
            return new GroupVMList(groups.Select(f=>f.ToGroup()));
        }

        internal static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Group> items, List<UserGroup> selected)
        {
            return new SelectableItemList<int>(items.Select(i => i.ToSelectableItem(selected)));
        }
    }
}

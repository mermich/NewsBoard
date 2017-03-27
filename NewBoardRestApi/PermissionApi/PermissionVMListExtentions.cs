using NewBoardRestApi.DataModel;
using Selectable;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.PermissionApi
{
    internal static class PermissionVMListExtentions
    {
        internal static PermissionVMList ToPermissionVMList(this IEnumerable<Permission> items)
        {
            return new PermissionVMList(items.Select(i => i.ToPermission()));
        }

        internal static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Permission> items, List<GroupPermission> selected)
        {
            return new SelectableItemList<int>(items.Select(i => i.ToSelectableItem(selected)));
        }
    }
}

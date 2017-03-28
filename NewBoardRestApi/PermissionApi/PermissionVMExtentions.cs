using NewBoardRestApi.DataModel;
using ApiTools.Selectable;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.PermissionApi
{
    internal static class PermissionVMExtentions
    {
        internal static PermissionVM ToPermission(this Permission item)
        {
            return new PermissionVM(item);
        }

        internal static List<PermissionVM> ToPermissions(this List<Permission> items)
        {
            return items.Select(i => i.ToPermission()).ToList();
        }


        internal static SelectableItem<int> ToSelectableItem(this Permission item, List<GroupPermission> selected)
        {
            return new SelectableItem<int>(item.Id, item.Label, selected.Any(at => at.PermissionId == item.Id));
        }
    }
}

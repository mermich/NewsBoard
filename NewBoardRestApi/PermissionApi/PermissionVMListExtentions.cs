using NewBoardRestApi.DataModel;
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
    }
}

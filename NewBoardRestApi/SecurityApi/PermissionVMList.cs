using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class PermissionVMList
    {
        public List<PermissionVM> Permissions { get; set; }

        public PermissionVMList() { }

        public PermissionVMList(IEnumerable<PermissionVM> permissions)
        {
            Permissions = permissions.ToList();
        }
    }


    internal static class PermissionsVMListExtentions
    {
        internal static PermissionVMList ToPermissionVMList(this IEnumerable<Permission> items)
        {
            return new PermissionVMList(items.Select(i => i.ToPermission()));
        }
    }
}

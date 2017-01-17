using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
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


    public static class PermissionsVMListExtentions
    {
        public static PermissionVMList ToPermissionVMList(this IEnumerable<Permission> items)
        {
            return new PermissionVMList(items.Select(i => i.ToPermission()));
        }
    }
}

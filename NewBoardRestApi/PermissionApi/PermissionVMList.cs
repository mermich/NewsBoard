using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.PermissionApi
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
}

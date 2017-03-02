using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.GroupApi
{
    internal static class GroupEditVMExtentions
    {
        internal static GroupEditVM ToGroupEditVM(this Group item, List<Permission> allPermissions)
        {
            return new GroupEditVM(item, allPermissions);
        }
    }

}

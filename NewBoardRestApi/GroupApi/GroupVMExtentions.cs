using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.GroupApi
{
    internal static class GroupVMExtentions
    {
        internal static GroupVM ToGroup(this Group item)
        {
            return new GroupVM(item);
        }

        internal static List<GroupVM> ToGroups(this List<Group> items)
        {
            return items.Select(i => i.ToGroup()).ToList();
        }
    }

}

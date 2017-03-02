using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.GroupApi
{
    internal static class GroupVMListExtentions
    {
        internal static GroupVMList ToGroupVMList(this IEnumerable<Group> items)
        {
            return new GroupVMList(items.Select(i => i.ToGroup()));
        }
    }
}

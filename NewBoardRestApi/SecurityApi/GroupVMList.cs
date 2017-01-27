using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class GroupVMList
    {
        public List<GroupVM> Groups { get; set; }

        public GroupVMList() { }

        public GroupVMList(IEnumerable<GroupVM> groups)
        {
            Groups = groups.ToList();
        }
    }


    internal static class GroupVMListExtentions
    {
        internal static GroupVMList ToGroupVMList(this IEnumerable<Group> items)
        {
            return new GroupVMList(items.Select(i => i.ToGroup()));
        }
    }
}

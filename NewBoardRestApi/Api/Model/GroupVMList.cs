using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
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


    public static class GroupVMListExtentions
    {
        public static GroupVMList ToGroupVMList(this IEnumerable<Group> items)
        {
            return new GroupVMList(items.Select(i => i.ToGroup()));
        }
    }
}

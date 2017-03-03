using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.GroupApi
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
}

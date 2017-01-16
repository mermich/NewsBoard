using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class GroupVM
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Permissions { get; set; }


        public GroupVM()
        {
        }

        public GroupVM(Group group)
        {
            Id = group.Id;
            Label = group.Label;
            Permissions = string.Join(",", group.GroupPermissions.Select(gp => gp.Permission.Label));
        }
    }

    public static class GroupVMExtentions
    {
        public static GroupVM ToGroup(this Group item)
        {
            return new GroupVM(item);
        }

        public static List<GroupVM> ToGroups(this List<Group> items)
        {
            return items.Select(i => i.ToGroup()).ToList();
        }
    }

}

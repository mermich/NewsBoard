using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class GroupEditVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public bool Enabled { get; set; }

        public SelectableItemList Permissions { get; set; }

        public GroupEditVM()
        {
        }

        public GroupEditVM(List<Permission> allPermissions)
        {
            Permissions = allPermissions.ToSelectableItemList(new List<GroupPermission>());
        }

        public GroupEditVM(Group group, List<Permission> allPermissions)
        {
            Id = group.Id;
            Label = group.Label;
            Permissions = allPermissions.ToSelectableItemList(group.GroupPermissions);
        }
    }

    public static class GroupEditVMExtentions
    {
        public static GroupEditVM ToGroupEditVM(this Group item, List<Permission> allPermissions)
        {
            return new GroupEditVM(item, allPermissions);
        }
    }

}

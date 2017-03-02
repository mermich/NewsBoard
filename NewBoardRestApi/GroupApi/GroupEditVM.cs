using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.GroupApi
{
    public class GroupEditVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public bool Enabled { get; set; }

        public SelectableItemList Permissions { get; set; } = new SelectableItemList();

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
}

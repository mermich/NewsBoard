﻿using ApiTools.Selectable;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using NewBoardRestApi.PermissionApi;

namespace NewBoardRestApi.GroupApi
{
    public class GroupEditVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public bool Enabled { get; set; }

        public SelectableItemList<int> Permissions { get; set; } = new SelectableItemList<int>();

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

﻿using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class PermissionVM
    {
        public int Id { get; set; }


        public string Label { get; set; } = "";


        public string Key { get; set; } = "";


        public PermissionVM()
        {
        }


        public PermissionVM(Permission perm)
        {
            Id = perm.Id;
            Label = perm.Label;
            Key = perm.Key;
        }
    }


    internal static class PermissionVMExtentions
    {
        internal static PermissionVM ToPermission(this Permission item)
        {
            return new PermissionVM(item);
        }

        internal static List<PermissionVM> ToPermissions(this List<Permission> items)
        {
            return items.Select(i => i.ToPermission()).ToList();
        }
    }
}
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class PermissionVM
    {
        public int Id { get; set; }


        public string Label { get; set; }


        public string Key { get; set; }


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


    public static class PermissionVMExtentions
    {
        public static PermissionVM ToPermission(this Permission item)
        {
            return new PermissionVM(item);
        }

        public static List<PermissionVM> ToPermissions(this List<Permission> items)
        {
            return items.Select(i => i.ToPermission()).ToList();
        }
    }
}

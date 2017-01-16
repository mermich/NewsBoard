
using System.Collections.Generic;

namespace NewBoardRestApi.DataModel
{
    public class Permission
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Key { get; set; }

        public List<GroupPermission> GroupPermissions { get; set; }
    }
}
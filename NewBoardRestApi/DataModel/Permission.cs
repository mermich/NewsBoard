
using System.Collections.Generic;

namespace NewBoardRestApi.DataModel
{
    public class Permission
    {
        public virtual int Id { get; set; }

        public virtual string Label { get; set; }

        public List<GroupPermission> GroupPermissions { get; set; }

        public Permission()
        {
        }
    }
}

using System.Collections.Generic;

namespace NewBoardRestApi.DataModel
{
    public class Group
    {
        public virtual int Id { get; set; }

        public virtual string Label { get; set; }

        public List<GroupPermission> GroupPermissions { get; set; }

        public List<UserGroup> UserGroups { get; set; }


        public Group()
        {
        }
    }
}
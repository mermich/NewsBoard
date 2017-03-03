using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.GroupApi
{
    public class GroupVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public string Permissions { get; set; } = "";


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
}

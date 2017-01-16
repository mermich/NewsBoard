using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.Api.Model;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.Api
{
    public class GroupApi : BaseAuthenticatedApi
    {
        public GroupApi(string userToken = "") : base(userToken)
        {
        }

        public GroupVMList GetGroups()
        {
            return NewsBoardContext
                .Groups
                .Include(t => t.GroupPermissions)
                .ToGroupVMList();
        }


        public GroupEditVM GetNewGroupEditVM()
        {
            var permissions = NewsBoardContext.Permissions
                .Include(p => p.GroupPermissions)
                .ToList();

            return new GroupEditVM(permissions);
        }


        public GroupVM GetGroup(int groupId)
        {
            return NewsBoardContext
                .Groups
                .Include(t => t.GroupPermissions)
                .FirstOrDefault(t => t.Id == groupId)
                .ToGroup();
        }


        public GroupEditVM GetEditGroup(int groupId)
        {
            var permissions = NewsBoardContext.Permissions
                .Include(p => p.GroupPermissions)
                .ToList();


            return NewsBoardContext
                   .Groups
                   .Include(t => t.GroupPermissions)
                   .FirstOrDefault(t => t.Id == groupId)
                   .ToGroupEditVM(permissions);
        }

        public GroupVM CreateGroup(GroupEditVM groupVM)
        {
            var group = new Group
            {
                Label = groupVM.Label
            };

            foreach (var item in groupVM.Permissions.Items.Where(i=>i.IsSelected))
            {
                group.GroupPermissions.Add(new GroupPermission { Group = group, PermissionId = item.Id });
            }

            NewsBoardContext.Groups.Add(group);
            NewsBoardContext.SaveChanges();

            return GetGroup(group.Id);
        }


        public GroupVM SaveGroup(GroupEditVM groupVM)
        {
            var group = NewsBoardContext.Groups.FirstOrDefault(t => t.Id == groupVM.Id);
            group.Label = groupVM.Label;

            // TODO MERGE PERMISSSIONS.
            
            NewsBoardContext.SaveChanges();

            return GetGroup(group.Id);
        }
    }
}

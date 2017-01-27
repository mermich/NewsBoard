using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class GroupApi : BaseAuthenticatedApi
    {
        public GroupApi(int userId) : base(userId)
        {
        }

        public GroupVMList GetGroups()
        {
            return NewsBoardContext
                .Groups
                .Include(t => t.GroupPermissions).ThenInclude(gp => gp.Permission)
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
                .Include(t => t.GroupPermissions).ThenInclude(gp => gp.Permission)
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
                Label = groupVM.Label,
                GroupPermissions = new List<GroupPermission>()
            };

            foreach (var item in groupVM.Permissions.Items.Where(i => i.IsSelected))
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

            var selectedPermissions = groupVM.Permissions.Items.Where(gr => gr.IsSelected);

            //removing the old permissions
            foreach (var permission in group.GroupPermissions.ToList())
            {
                if (!selectedPermissions.Any(gr => gr.Id == permission.PermissionId))
                {
                    //not in the posted list i should delete the item
                    //I remove the item from the dbcontext rather than from the dbItem
                    //  otherwise it will try to set the foreign key column to null instead of deleting the row. 
                    NewsBoardContext.GroupPermissions.Remove(permission);
                }
            }

            //adding the new ones
            foreach (var permission in selectedPermissions)
            {
                //if is not in database
                if (!group.GroupPermissions.Any(a => a.PermissionId == permission.Id))
                {
                    //create the row
                    var gp = new GroupPermission();
                    gp.Group = group;
                    gp.Permission = NewsBoardContext.Permissions.FirstOrDefault(a => a.Id == permission.Id);
                    group.GroupPermissions.Add(gp);
                }
            }

            NewsBoardContext.SaveChanges();

            return GetGroup(group.Id);
        }
    }
}

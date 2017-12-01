using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using NewBoardRestApi.DataModel.Engine;

namespace NewBoardRestApi.GroupApi
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
                group.GroupPermissions.Add(new GroupPermission { Group = group, PermissionId = item.Value });
            }

            NewsBoardContext.Groups.Add(group);
            NewsBoardContext.SaveChanges();

            return GetGroup(group.Id);
        }


        public GroupVM SaveGroup(GroupEditVM groupVM)
        {
            var group = NewsBoardContext
                .Groups
                .Include(g => g.GroupPermissions)
                .ThenInclude(gp => gp.Permission)
                .FirstOrDefault(t => t.Id == groupVM.Id);

            group.Label = groupVM.Label;

            var selectedPermissions = groupVM.Permissions.SelectedValues;

            Func<GroupPermission, int> existingIdentifier = g => g.PermissionId;
            Func<int, GroupPermission> convertFunc = g => new GroupPermission
            {
                Group = group,
                Permission = NewsBoardContext.Permissions.FirstOrDefault(a => a.Id == g)
            };
            
            NewsBoardContext.GroupPermissions.MergeLists(existingIdentifier, selectedPermissions, convertFunc);

            NewsBoardContext.SaveChanges();

            return GetGroup(group.Id);
        }
    }
}
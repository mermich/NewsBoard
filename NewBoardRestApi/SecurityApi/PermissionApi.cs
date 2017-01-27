using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class PermissionApi : BaseAuthenticatedApi
    {
        public PermissionApi(int userId) : base(userId)
        {
        }

        public PermissionVMList GetPermissions()
        {
            return NewsBoardContext.Permissions.ToPermissionVMList();
        }


        public PermissionVM GetNewPermissionEditVM()
        {
            return new PermissionVM();
        }


        public PermissionVM GetPermission(int permissionId)
        {
            return NewsBoardContext
                .Permissions
                .FirstOrDefault(t => t.Id == permissionId)
                .ToPermission();
        }
        

        public PermissionVM CreatePermission(PermissionVM permissionVM)
        {
            var permission = new Permission
            {
                Label = permissionVM.Label,
                Key = permissionVM.Key,
            };
            
            NewsBoardContext.Permissions.Add(permission);
            NewsBoardContext.SaveChanges();

            return GetPermission(permission.Id);
        }


        public PermissionVM SavePermission(PermissionVM permissionVM)
        {
            var permission = NewsBoardContext.Permissions.FirstOrDefault(t => t.Id == permissionVM.Id);
            permission.Label = permissionVM.Label;
            permission.Key = permissionVM.Key;

            NewsBoardContext.SaveChanges();

            return GetPermission(permission.Id);
        }
    }
}

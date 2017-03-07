using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.UserApi
{
    public class UserApi : BaseAuthenticatedApi
    {
        public UserApi(int userId) : base(userId)
        {
        }

        public UserApi(User user) : base(user)
        {

        }


        internal User Get()
        {
            return currentUser;
        }


        public UserVMList ListUsers()
        {
            return NewsBoardContext.Users
                 .Include(u => u.UserGroups).ThenInclude(ug => ug.Group)
                 .ToUserVMList();
        }


        public UserEditVM GetNewUserEditVM()
        {
            var groups = NewsBoardContext.Groups.ToList();
            return new UserEditVM(groups);
        }

        public UserVM CreateUser(UserEditVM userVM)
        {
            var user = new User
            {
                Email = userVM.Email,
                Password = userVM.Password,
            };

            foreach (var item in user.UserGroups)
            {
                user.UserGroups.Add(new UserGroup { GroupId = item.Id, User = user });
            }

            NewsBoardContext.Users.Add(user);
            NewsBoardContext.SaveChanges();

            return GetUser(user.Id);
        }


        public UserVM SaveUser(UserEditVM userVM)
        {
            var user = NewsBoardContext.Users
                .Include(u => u.UserGroups).ThenInclude(ug => ug.Group)
                .FirstOrDefault(t => t.Id == userVM.Id);
            user.Email = userVM.Email;
            user.Password = userVM.Password;

            var selectedGroups = userVM.Groups.Items.Where(gr => gr.IsSelected);

            //removing the old permissions
            foreach (var userGroup in user.UserGroups.ToList())
            {
                if (!selectedGroups.Any(gr => gr.Value == userGroup.GroupId))
                {
                    //not in the posted list i should delete the item
                    //I remove the item from the dbcontext rather than from the dbItem
                    //  otherwise it will try to set the foreign key column to null instead of deleting the row. 
                    NewsBoardContext.UserGroups.Remove(userGroup);
                }
            }

            //adding the new ones
            foreach (var group in selectedGroups)
            {
                //if is not in database
                if (!user.UserGroups.Any(a => a.GroupId == group.Value))
                {
                    //create the row
                    var ug = new UserGroup();
                    ug.User = user;
                    ug.Group = NewsBoardContext.Groups.FirstOrDefault(a => a.Id == group.Value);
                    user.UserGroups.Add(ug);
                }
            }

            NewsBoardContext.SaveChanges();

            return GetUser(user.Id);
        }

        public UserVM GetUser(int userId)
        {
            return NewsBoardContext.Users
                .Include(u => u.UserGroups).ThenInclude(ug => ug.Group)
                .FirstOrDefault(t => t.Id == userId).ToUserVM();
        }

        public UserEditVM GetUserEdit(int userId)
        {
            var groups = NewsBoardContext.Groups.ToList();

            return NewsBoardContext.Users
                .Include(u => u.UserGroups).ThenInclude(ug => ug.Group)
                .FirstOrDefault(t => t.Id == userId).ToUserEditVM(groups);
        }
    }
}
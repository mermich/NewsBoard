using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.Api.Model;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.Api
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

            // TODO MERGE GROUPS

            NewsBoardContext.SaveChanges();
            
            return GetUser(user.Id);
        }

        public UserVM GetUser(int userId)
        {
            return NewsBoardContext.Users
                .Include(u=>u.UserGroups).ThenInclude(ug=>ug.Group)
                .FirstOrDefault(t => t.Id == userId).ToUserVM();
        }
    }
}
using ApiTools;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.UserApi
{
    public class AuthenticationApi : BaseApi.BaseApi
    {
        public User Register(UserRegisterVM model)
        {
            if (NewsBoardContext.Users.Any(u => u.Email == model.Email))
            {
                throw new BusinessLogicException("Un utilisateur existe deja avec cet email.");
            }
            else
            {
                var user = new User();
                user.Email = model.Email;
                user.Password = model.Password;

                NewsBoardContext.Users.Add(user);
                NewsBoardContext.SaveChanges();

                return user;
            }
        }

        public UserVM Login(UserLoginVM model)
        {
            var user = NewsBoardContext.Users
                .Include(u => u.UserGroups).ThenInclude(ug => ug.Group).ThenInclude(g => g.GroupPermissions).ThenInclude(gp => gp.Permission)
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                throw new BusinessLogicException("Login/Mode de passe incorrects.");
            }
            else
            {
                return new UserVM(user);
            }
        }

        public UserLoginVM GetNewUserLoginVM()
        {
            return new UserLoginVM();
        }

        public List<string> GetPermissions(int userId)
        {
            var result = NewsBoardContext.Groups.Where(g => g.UserGroups.Any(ug => ug.UserId == userId))
                 .SelectMany(g => g.GroupPermissions.Select(gp => gp.Permission.Key)).ToList();


            return result;
        }
    }
}
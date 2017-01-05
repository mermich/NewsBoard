using System;
using System.Linq;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.Api.Model;

namespace NewBoardRestApi.Api
{
    public class AuthenticationApi : BaseApi
    {
        public User Register(UserRegisterVM model)
        {
            if (NewsBoardContext.Users.Any(u => u.Email == model.Email))
            {
                throw new Exception();
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

        public int Login(UserLoginVM model)
        {
            var user = NewsBoardContext.Users.FirstOrDefault(u => u.Email == model.Email);
            if(user != null)
                return user.Id;

            throw new Exception();
        }

        public UserLoginVM GetNewUserLoginVM()
        {
            return new UserLoginVM();
        }
    }
}
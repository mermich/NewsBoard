using System;
using System.Linq;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api
{
    public class UserRegisterApi : BaseApi
    {
        public User Register(string email, string password)
        {
            if (NewsBoardContext.Users.Any(u => u.Email == email))
            {
                throw new Exception();
            }
            else
            {
                var user = new User();
                user.Email = email;
                user.Password = password;

                NewsBoardContext.Users.Add(user);
                NewsBoardContext.SaveChanges();

                return user;
            }
        }
    }
}
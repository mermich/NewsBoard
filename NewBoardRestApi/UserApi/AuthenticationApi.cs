using ApiTools;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.DataModel;
using SendGrid;
using SendGrid.Helpers.Mail;
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
                var user = new User
                {
                    Email = model.Email,
                    Password = model.Password
                };

                NewsBoardContext.Users.Add(user);
                NewsBoardContext.SaveChanges();

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("emd747@gmail.com", "News Board Admin !"),
                    Subject = "Hello World from the NewsBoard!",
                    PlainTextContent = "Hello, Email!",
                    HtmlContent = $@"<strong>Hello, Email</strong>"
                };
                msg.AddTo(new EmailAddress(model.Email));

                // dat key is invalid
                var client = new SendGridClient("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");
                //client.SendEmailAsync(msg);

                return user;
            }
        }



        public UserVM Login(UserLoginVM model)
        {
            var user = NewsBoardContext.Users
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .ThenInclude(g => g.GroupPermissions)
                .ThenInclude(gp => gp.Permission)
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
            var result = NewsBoardContext
                .Groups
                .Where(g => g.UserGroups.Any(ug => ug.UserId == userId))
                .SelectMany(g => g.GroupPermissions.Select(gp => gp.Permission.Key))
                .ToList();


            return result;
        }
    }
}
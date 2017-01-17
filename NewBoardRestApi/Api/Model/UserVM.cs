using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class UserVM
    {
        public string Email { get; set; }

        public int Id { get; set; }

        public string  Password { get; set; }

        public List<string> Groups { get; set; } = new List<string>();


        public UserVM()
        {

        }

        public UserVM(User user) :this()
        {
            Email = user.Email;
            Id = user.Id;
            Password = user.Password;
            Groups.AddRange(user.UserGroups.Select(g => g.Group.Label));
        }
    }

    public static class UserVMExtenetions
    {
        public static UserVM ToUserVM(this User user)
        {
            return new UserVM(user);
        }
    }
}

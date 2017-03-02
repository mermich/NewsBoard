using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.UserApi
{
    public class UserVM
    {
        public string Email { get; set; } = "";

        public int Id { get; set; }

        public string Password { get; set; } = "";

        public List<string> Groups { get; set; } = new List<string>();

        public string GroupString
        {
            get
            {
                return string.Join(", ", Groups.Select(g => g));
            }
        }


        public UserVM()
        {

        }

        public UserVM(User user) : this()
        {
            Email = user.Email;
            Id = user.Id;
            Password = user.Password;
            Groups.AddRange(user.UserGroups.Select(g => g.Group.Label));
        }
    }
}

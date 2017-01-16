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

        public List<string> Permissions { get; set; } = new List<string>();


        public UserVM()
        {

        }

        public UserVM(User user) :this()
        {
            Email = user.Email;
            Id = user.Id;

            foreach (var item in user.UserGroups.SelectMany(ug => ug.Group.GroupPermissions.Select(gp => gp.Permission)))
            {
                Permissions.Add(item.Key);
            }
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

using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.SecurityApi
{
    public class UserVMList
    {
        public List<UserVM> Users { get; set; }

        public UserVMList() { }

        public UserVMList(IEnumerable<UserVM> users)
        {
            Users = users.ToList();
        }
    }


    internal static class UserVMListExtentions
    {
        internal static UserVMList ToUserVMList(this IEnumerable<User> items)
        {
            return new UserVMList(items.Select(i => i.ToUserVM()));
        }
    }
}

using NewBoardRestApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
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


    public static class UserVMListExtentions
    {
        public static UserVMList ToUserVMList(this IEnumerable<User> items)
        {
            return new UserVMList(items.Select(i => i.ToUserVM()));
        }
    }
}

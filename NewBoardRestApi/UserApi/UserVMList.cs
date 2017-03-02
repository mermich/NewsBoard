using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.UserApi
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
}

using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.UserApi
{
    internal static class UserVMExtentions
    {
        internal static UserVM ToUserVM(this User user)
        {
            return new UserVM(user);
        }
    }
}

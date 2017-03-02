using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.UserApi
{
    internal static class UserVMListExtentions
    {
        internal static UserVMList ToUserVMList(this IEnumerable<User> items)
        {
            return new UserVMList(items.Select(i => i.ToUserVM()));
        }
    }
}

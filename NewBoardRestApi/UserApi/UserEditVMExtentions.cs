using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.UserApi
{
    internal static class UserEditVMExtentions
    {
        internal static UserEditVM ToUserEditVM(this User user, List<Group> allGroups)
        {
            return new UserEditVM(user, allGroups);
        }
    }
}

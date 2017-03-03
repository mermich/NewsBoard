using NewBoardRestApi.DataModel;

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

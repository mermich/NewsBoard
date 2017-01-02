using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api
{
    public class UserApi : BaseAuthenticatedApi
    {
        public UserApi(string userToken) : base(userToken)
        {
        }

        public UserApi(User user) :base(user)
        {
        }

        public User Get()
        {
            return currentUser;
        }        
    }
}
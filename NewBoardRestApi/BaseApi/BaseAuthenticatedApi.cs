using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.BaseApi
{
    public class BaseAuthenticatedApi : BaseApi
    {
        protected User currentUser;

        public BaseAuthenticatedApi(int userId)
        {
            currentUser = NewsBoardContext.Users.FirstOrDefault(u => u.Id == userId);
        }

        public BaseAuthenticatedApi(User user)
        {
            currentUser = user;
        }
    }
}

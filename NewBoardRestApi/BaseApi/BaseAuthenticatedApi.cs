using ApiTools;

namespace NewBoardRestApi.BaseApi
{
    public class BaseAuthenticatedApi : BaseApi
    {
        public static readonly int UnAuthenticatedUserId = 0;

        public readonly int UserId;

        public BaseAuthenticatedApi(int userId)
        {
            this.UserId = userId;
        }

        public void ThrowExIfUnAuthenticated()
        {
            if (IsAnonymous)
            {
                throw new NeedAuthenticationException();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return UserId != UnAuthenticatedUserId;
            }
        }

        public bool IsAnonymous
        {
            get
            {
                return UserId == UnAuthenticatedUserId;
            }
        }
    }
}

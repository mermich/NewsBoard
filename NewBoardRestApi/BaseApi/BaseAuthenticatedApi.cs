using ApiTools;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.BaseApi
{
    public class BaseAuthenticatedApi : BaseApi
    {
        public static readonly int UnAuthenticatedUserId = 0;

        public readonly int UserId;

        public List<int> UserFeeds { get; set; } = new List<int>();

        public BaseAuthenticatedApi(NewsBoardContext newsBoardContext, SessionObject sessionObject) : base(newsBoardContext)
        {
            this.UserId = sessionObject.UserId;
            this.UserFeeds = sessionObject.UserFeeds;
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

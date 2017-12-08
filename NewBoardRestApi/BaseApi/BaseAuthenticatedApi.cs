using ApiTools;
using Microsoft.AspNetCore.Http;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi
{
    public class SessionObject
    {
        public int UserId { get; set; }

        public SessionObject(ISession isession)
        {
            UserId = isession.GetInt32("UserId").GetValueOrDefault();
        }
    }
}


namespace NewBoardRestApi.BaseApi
{
    public class BaseAuthenticatedApi : BaseApi
    {
        public static readonly int UnAuthenticatedUserId = 0;

        public readonly int UserId;

        public BaseAuthenticatedApi(NewsBoardContext newsBoardContext, SessionObject sessionObject) : base(newsBoardContext)
        {
            this.UserId = sessionObject.UserId;
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

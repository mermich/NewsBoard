using System;
using System.Collections.Generic;

namespace NewBoardRestApi.BaseApi
{
    public class NeedAuthenticationException : BusinessLogicException
    {
        public NeedAuthenticationException() : base("User needs to be authenticated to perfom the action.") { }

    }
}

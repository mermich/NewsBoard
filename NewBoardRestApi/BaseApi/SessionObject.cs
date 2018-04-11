using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.BaseApi
{
    public class SessionObject
    {
        public int UserId { get; set; }

        public List<int> UserFeeds { get; set; } = new List<int>();

        public SessionObject(ISession iSession)
        {
            UserId = iSession.GetInt32("UserId").GetValueOrDefault();


            if(!string.IsNullOrWhiteSpace(iSession.GetString("UserFeeds")))
            {
                UserFeeds = iSession.GetString("UserFeeds").Split("_").ToArray().Select(s => int.Parse(s)).ToList();
            }
                
        }
    }
}
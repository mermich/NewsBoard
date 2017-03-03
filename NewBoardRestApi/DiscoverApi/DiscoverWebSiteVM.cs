using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.DiscoverApi
{
    public class DiscoverWebSiteVM
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public List<DiscoverFeedVM> Feeds { get; set; } = new List<DiscoverFeedVM>();

        public DiscoverWebSiteVM()
        {

        }
    }
}
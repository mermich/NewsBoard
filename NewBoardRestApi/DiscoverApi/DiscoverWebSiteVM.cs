using System.Collections.Generic;

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
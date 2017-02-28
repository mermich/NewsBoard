using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.FeedApi
{

    public class FeedVMList
    {
        public FeedVMListOptions Options { get; set; } = new FeedVMListOptions();

        public List<FeedVM> Feeds { get; set; } = new List<FeedVM>();

        public FeedVMList()
        {

        }

        public FeedVMList(IEnumerable<FeedVM> feeds)
        {
            Feeds = feeds.ToList();
        }
    }
}

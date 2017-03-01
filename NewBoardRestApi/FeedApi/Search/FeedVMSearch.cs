using System.Collections.Generic;


namespace NewBoardRestApi.FeedApi.Search
{
    public class FeedVMSearch
    {
        public List<int> Tags { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 20;

        public bool OnlyUserSubscription { get; set; } = true;

        public bool HideReported { get; set; } = true;

        public FeedListOrderBy OrderBy { get; set; } = FeedListOrderBy.Name;
    }
}

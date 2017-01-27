using System;
using System.Collections.Generic;

namespace NewBoardRestApi.FeedApi
{
    public class FeedVMSearch
    {
        public List<int> Tags { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 20;

        public bool OnlyUserSubscription { get; set; } = true;

        public bool HideReported { get; set; } = true;

        public FeedListOrderBy OrderBy { get; set; } = FeedListOrderBy.Name;
    }

    public abstract class FeedListOrderBy
    {
        public abstract string GetFilter();

        public static FeedListOrderByName Name => new FeedListOrderByName();

        public static FeedListOrderByPopularityAsc PopularityAsc => new FeedListOrderByPopularityAsc();

        public static FeedListOrderByPopularityDesc PopularityDesc => new FeedListOrderByPopularityDesc();
    }

    public class FeedListOrderByPopularityAsc : FeedListOrderBy
    {
        public override string GetFilter()
        {
            throw new NotImplementedException();
        }
    }


    public class FeedListOrderByName : FeedListOrderBy
    {
        public override string GetFilter()
        {
            throw new NotImplementedException();
        }
    }

    public class FeedListOrderByPopularityDesc : FeedListOrderBy
    {
        public override string GetFilter()
        {
            throw new NotImplementedException();
        }
    }
}

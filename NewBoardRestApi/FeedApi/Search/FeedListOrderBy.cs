using System;
using System.Collections.Generic;

namespace NewBoardRestApi.FeedApi.Search
{
    public abstract class FeedListOrderBy
    {
        public abstract string GetFilter();

        public static FeedListOrderByName Name => new FeedListOrderByName();

        public static FeedListOrderByPopularityAsc PopularityAsc => new FeedListOrderByPopularityAsc();

        public static FeedListOrderByPopularityDesc PopularityDesc => new FeedListOrderByPopularityDesc();
    }
}

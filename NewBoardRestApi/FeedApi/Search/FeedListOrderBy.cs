using System.Linq;


namespace NewBoardRestApi.FeedApi.Search
{
    public abstract class FeedListOrderBy
    {
        public abstract IQueryable<NewBoardRestApi.DataModel.Feed> Filter(IQueryable<NewBoardRestApi.DataModel.Feed> query);

        public static FeedListOrderByName Name => new FeedListOrderByName();

        public static FeedListOrderByPopularityAsc PopularityAsc => new FeedListOrderByPopularityAsc();

        public static FeedListOrderByPopularityDesc PopularityDesc => new FeedListOrderByPopularityDesc();
    }

   
}

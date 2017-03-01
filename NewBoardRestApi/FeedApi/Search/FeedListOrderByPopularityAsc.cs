using NewBoardRestApi.DataModel;
using System.Linq;


namespace NewBoardRestApi.FeedApi.Search
{
    public class FeedListOrderByPopularityAsc : FeedListOrderBy
    {
        public override IQueryable<Feed> Filter(IQueryable<Feed> query)
        {
             return query.OrderBy(f=>f.UserFeeds.Where(t=>t.IsSubscribed).Count());
        }
    }
}

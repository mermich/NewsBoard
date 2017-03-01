using NewBoardRestApi.DataModel;
using System;
using System.Linq;


namespace NewBoardRestApi.FeedApi.Search
{
    public class FeedListOrderByName : FeedListOrderBy
    {
        public override IQueryable<Feed> Filter(IQueryable<Feed> query)
        {
            throw new NotImplementedException();
        }

    }
}

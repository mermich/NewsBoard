using System;
using System.Collections.Generic;
using System.Linq;
using NewBoardRestApi.DataModel;
using System;
using NewBoardRestApi.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using SiteParser;
using NewBoardRestApi.ArticleApi;


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

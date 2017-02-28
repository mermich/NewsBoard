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
    public abstract class FeedListOrderBy
    {
        public abstract IQueryable<NewBoardRestApi.DataModel.Feed> Filter(IQueryable<NewBoardRestApi.DataModel.Feed> query);

        public static FeedListOrderByName Name => new FeedListOrderByName();

        public static FeedListOrderByPopularityAsc PopularityAsc => new FeedListOrderByPopularityAsc();

        public static FeedListOrderByPopularityDesc PopularityDesc => new FeedListOrderByPopularityDesc();
    }

   
}

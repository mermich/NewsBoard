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
    public class FeedListOrderByName : FeedListOrderBy
    {
        public override IQueryable<Feed> Filter(IQueryable<Feed> query)
        {
            throw new NotImplementedException();
        }

    }
}

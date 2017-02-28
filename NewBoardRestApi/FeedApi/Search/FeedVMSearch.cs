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
    public class FeedVMSearch
    {
        public List<int> Tags { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 20;

        public bool OnlyUserSubscription { get; set; } = true;

        public bool HideReported { get; set; } = true;

        public FeedListOrderBy OrderBy { get; set; } = FeedListOrderBy.Name;
    }
}

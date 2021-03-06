﻿using System.Collections.Generic;

namespace NewBoardRestApi.FeedApi.Search
{
    public class FeedVMSearch
    {
        public List<int> Tags { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 10;

        public bool ShouldPickRandomItems { get; set; } = false;

        public SubscriptionFilter SubscriptionFilter { get; set; } = SubscriptionFilter.OnlySubscribbed;

        public bool HideReported { get; set; } = true;

        public FeedListOrderBy OrderBy { get; set; } = FeedListOrderBy.Name;
    }
}

﻿using NewBoardRestApi.ArticleApi;
using SiteParser.Client;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.DiscoverApi
{
    public static class DiscoverWebSiteVMExtentions
    {
        internal static DiscoverWebSiteVM ToDiscoverWebSiteVM(this int item)
        {
            return new DiscoverWebSiteVM();
        }
    }
}
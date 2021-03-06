﻿using ApiTools.HttpTools;
using System.Linq;
using System;

namespace ApiTools.SyndicationSearch
{
    public class WordPressSyndicationSearch : ASyndicationSearch
    {
        public WordPressSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override Uri GetSyndicationUri()
        {
            var feedNode = doc.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault();

            return new Uri(doc.Uri, feedNode.GetAttributeValue("href"));
        }

        public override int MatchScore()
        {
            bool isWordpress = doc.GetNodesByExpression("//meta[@content]").Any(m => m.GetAttributeValue("content").Contains("WordPress"));
            if(isWordpress)
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ApiTools.HttpTools;

namespace ApiTools.SyndicationSearch
{
    public class SyndicationSearchStrategy
    {
        List<ASyndicationSearch> searchers = new List<ASyndicationSearch>();
        ASyndicationSearch defaultSyndicationSearch;

        public SyndicationSearchStrategy(HtmlDocumentPageWrapper doc) : this(new DefaultSyndicationSearch(doc), new List<ASyndicationSearch> { new WordPressSyndicationSearch(doc), new YoutubeSyndicationSearch(doc) })
        {
        }

        public SyndicationSearchStrategy(ASyndicationSearch defaultSyndicationSearch, IList<ASyndicationSearch> searchers)
        {
            this.defaultSyndicationSearch = defaultSyndicationSearch;
            this.searchers.AddRange(searchers);
        }

        public ASyndicationSearch GetFeedSearch()
        {
            return searchers.FirstOrDefault(f => f.IsMatch());
        }

        public ASyndicationSearch GetFeedSearchOrDefault()
        {
            var finder = searchers.FirstOrDefault(f => f.IsMatch());
            if (finder == null)
            {
                finder = defaultSyndicationSearch;
            }

            return finder;
        }
    }
}

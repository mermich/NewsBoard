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

        public SyndicationSearchStrategy(HtmlDocumentPageWrapper doc) : this(new DefaultSyndicationSearch(doc), new List<ASyndicationSearch> { new WordPressSyndicationSearch(doc), new YoutubeSyndicationSearch(doc), new WordPressElegantThemesSyndicationSearch(doc), new LefigaroSyndicationSearch(doc) })
        {
        }

        public SyndicationSearchStrategy(ASyndicationSearch defaultSyndicationSearch, IList<ASyndicationSearch> searchers)
        {
            this.defaultSyndicationSearch = defaultSyndicationSearch;
            this.searchers.AddRange(searchers);
        }

        public ASyndicationSearch GetFeedSearch()
        {
            return searchers.OrderBy(f => f.MatchScore()).LastOrDefault();
        }

        public ASyndicationSearch GetFeedSearchOrDefault()
        {
            var finder = searchers.OrderBy(f => f.MatchScore()).LastOrDefault();
            if (finder.MatchScore() == 0)
            {
                finder = defaultSyndicationSearch;
            }

            return finder;
        }
    }
}

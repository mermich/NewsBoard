using System;
using System.Collections.Generic;
using System.Linq;
using ApiTools.HttpTools;

namespace ApiTools.IconSearch
{
    public class IconSearchStrategy
    {
        List<AIconSearch> searchers = new List<AIconSearch>();
        AIconSearch defaultSyndicationSearch;

        public IconSearchStrategy(HtmlDocumentPageWrapper doc) : this(new DefaultIconSearch(doc), new List<AIconSearch> { new YoutubeIconSearch(doc) })
        {
        }

        public IconSearchStrategy(AIconSearch defaultSyndicationSearch, IList<AIconSearch> searchers)
        {
            this.defaultSyndicationSearch = defaultSyndicationSearch;
            this.searchers.AddRange(searchers);
        }

        public AIconSearch GetFeedSearch()
        {
            return searchers.FirstOrDefault(f => f.IsMatch());
        }

        public AIconSearch GetIconSearchOrDefault()
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

using System.Collections.Generic;
using System.Linq;
using ApiTools.HttpTools;

namespace ApiTools.SyndicationSearch
{
    public class SyndicationSearchStrategy
    {
        List<ASyndicationSearch> searchers = new List<ASyndicationSearch>();

        public SyndicationSearchStrategy(HtmlDocumentPageWrapper doc) : this(new List<ASyndicationSearch> { new DefaultSyndicationSearch(doc), new MsdnBlogSyndicationSearch(doc), new WordPressSyndicationSearch(doc), new YoutubeSyndicationSearch(doc), new WordPressElegantThemesSyndicationSearch(doc), new LefigaroSyndicationSearch(doc) })
        {
        }

        public SyndicationSearchStrategy(IList<ASyndicationSearch> searchers)
        {
            this.searchers.AddRange(searchers);
        }

        public ASyndicationSearch GetFeedSearch()
        {
            return searchers.OrderBy(f => f.MatchScore()).LastOrDefault();
        }
    }
}

using ApiTools.HttpTools;
using System.Linq;
using System;

namespace ApiTools.SyndicationSearch
{
    public class LefigaroSyndicationSearch : ASyndicationSearch
    {
        public LefigaroSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override Uri GetSyndicationUri()
        {
            return new Uri("http://www.lefigaro.fr/rss/figaro_actualites.xml");
        }

        public override int MatchScore()
        {
            if( doc.Uri.Host == "www.lefigaro.fr")
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

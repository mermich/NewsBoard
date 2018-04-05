using ApiTools.HttpTools;
using ApiTools.IconSearch;
using ApiTools.SyndicationClient;
using ApiTools.SyndicationSearch;
using System;
using System.Linq;

namespace ApiTools
{

    public class LookupWebSiteApi
    {
        public string FindPageTitle(HtmlDocumentWrapper doc)
        {
            return doc.GetNodes("title").FirstOrDefault()?.InnerHtml;
        }

        public WebSiteDetails GetWebSiteDetails(Uri uri)
        {
            var hdoc = new HtmlDocumentPageWrapper(uri, new HttpClientWrapper(uri).FetchResponse());

            var details = new WebSiteDetails
            {
                Uri = uri,
                Title = FindPageTitle(hdoc).Trim(),
                SyndicationUri = new SyndicationSearchStrategy(hdoc).GetFeedSearch().GetSyndicationUri(),
                IconUri = new IconSearchStrategy(hdoc).GetIconSearchOrDefault().GetIconUri()
            };


            var xdoc = new XDocumentPageWrapper(details.SyndicationUri, new HttpClientWrapper(details.SyndicationUri).FetchResponse());
            var content = new SyndicationClientStrategy(xdoc).GetSyndicationClient().GetSyndicationContent();
            details.Description = content.Description.Trim();

            return details;
        }
    }
}

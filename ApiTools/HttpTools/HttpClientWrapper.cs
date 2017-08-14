using System;
using System.Net.Http;

namespace ApiTools.HttpTools
{
    public class HttpClientWrapper
    {
        public readonly Uri Uri;

        public HttpClientWrapper(Uri uri)
        {
            Uri = uri;
        }

        public string FetchResponse()
        {
            using (var client = new HttpClient())
            {
                var queryTask = client.GetStringAsync(Uri);
                queryTask.Wait();
                return queryTask.Result;
            };
        }

        public HtmlDocumentWrapper ToHtmlDocument()
        {
            HtmlDocumentWrapper htmlDoc = new HtmlDocumentWrapper(FetchResponse());
            return htmlDoc;
        }

        public XDocumentWrapper ToXDocument()
        {
            return new XDocumentWrapper(FetchResponse());
        }
    }
}
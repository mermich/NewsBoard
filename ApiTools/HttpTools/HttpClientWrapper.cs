using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

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
                var response = client.GetAsync(Uri);
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var res = response.Result.Content.ReadAsStringAsync();
                    res.Wait();
                    return RemoveEmptyLines(res.Result);
                }
                else
                {
                    throw new Exception("Didn't get a repsonse from:" + Uri + " error code:" + response.Result.StatusCode);
                }
            };
        }

        private string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd();
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
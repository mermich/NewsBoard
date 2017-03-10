using System.IO;
using System.Text;

namespace DiscoverWebSiteApi.HttpTools
{
    public class HttpClientResponse
    {
        string response = "";

        public HttpClientResponse() { }

        public HttpClientResponse(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                response = reader.ReadToEnd();
            }
        }
        

        public HtmlDocumentWrapper ToHtmlDocument()
        {
            HtmlDocumentWrapper htmlDoc = new HtmlDocumentWrapper(response);
            return htmlDoc;
        }

        public XDocumentWrapper ToXDocument()
        {
            return new XDocumentWrapper(response);
        }
    }
}
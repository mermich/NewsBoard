using NewBoardRestApi.Syndication;
using System.IO;
using System.Text;

namespace NewBoardRestApi.Http
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

        public XDocumentWrapper ToXDocument()
        {
            return new XDocumentWrapper(response);
        }

        public PageSyndicationFinder ToPageSyndicationFinder()
        {
            return new PageSyndicationFinder(response);
        }
    }
}
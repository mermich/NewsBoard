using System.IO;
using System.Text;
using System.Xml.Linq;

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

        public XDocument ToXDocument()
        {
            return XDocument.Parse(response);
        }

        public string ToDocument()
        {
            return response;
        }
    }
}
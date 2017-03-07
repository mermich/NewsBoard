using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            response = Regex.Replace(response, "&raquo;", "-");
            response = response.Replace("(<[^>]*[\\(.*?)([\"\\d\\w\\s])\\/>)", "$1></link>");
            response = Regex.Replace(response, @"<script[^>]*>[\s\S]*?</script>", "");
            Regex regex = new Regex(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", RegexOptions.Singleline);
            return XDocument.Parse(response);
        }

        public string ToDocument()
        {
            return response;
        }
    }
}
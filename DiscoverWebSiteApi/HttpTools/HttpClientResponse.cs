using System.Collections.Generic;
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
            response = Regex.Replace(response, "&rarr;", "-");
            response = Regex.Replace(response, "&larr;", "-");
            response = Regex.Replace(response, @"(<link[^>]+)(?<!/)(?=>)", "1/");
            response = Regex.Replace(response, @"(<img[^>]+)(?<!/)(?=>)", "1/");
            response = Regex.Replace(response, @"(<a[^>]+)(?<!/)(?=>)", "1/");

            response = Regex.Replace(response, @"<script[^>]*>[\s\S]*?</script>", "");
            response = Regex.Replace(response, @"<div[^>]*>[\s\S]*?</div>", "");
            response = Regex.Replace(response, @"<span[^>]*>[\s\S]*?</span>", "");
            response = Regex.Replace(response, @"<label[^>]*>[\s\S]*?</label>", "");
            response = Regex.Replace(response, @"<input[^>]*>[\s\S]*?</input>", "");

            response = Regex.Replace(response, @"required ", "required='required' ");
            response = Regex.Replace(response, @"[A-z]'t", " t");
            response = Regex.Replace(response, @"type= text/css'", "type='text/css'");
            response = Regex.Replace(response, @"type=text/css'", "type='text/css'");

            Regex regex = new Regex(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", RegexOptions.Singleline);
            return XDocument.Parse(response);
        }

        public string ToDocument()
        {
            return response;
        }
    }
}
using System;

namespace ApiTools.HttpTools
{
    public class UriPart
    {
        private string part;


        public UriPart(string part)
        {
            this.part = part;
        }


        public Uri ToFullUri(Uri baseUri)
        {
            return new Uri(baseUri, part);
        }
    }
}

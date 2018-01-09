using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ApiTools.HttpTools
{
    public class XDocumentWrapper
    {
        private XDocument xDocument;

        public XDocumentWrapper(string html) : this(new MemoryStream(Encoding.UTF8.GetBytes(html)))
        {
        }


        public XDocumentWrapper(Stream stream)
        {
            xDocument = XDocument.Load(stream);
        }

        

        public IEnumerable<XElement> Elements()
        {
            return xDocument.Elements();
        }

        public XElement Root()
        {
            return xDocument.Root;
        }
    }
}

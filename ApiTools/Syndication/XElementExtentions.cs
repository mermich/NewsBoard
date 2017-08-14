using System;
using System.Xml.Linq;

namespace ApiTools.Syndication
{
    public static class XElementExtentions
    {
        internal static string GetValueOrEmpty(this XElement element)
        {
            try
            {
                return element.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace NewsBoard.Tools
{
    public static class ObjectExtentions
    {
        public static IDictionary<string, object> ToIDictionary(this object anonymousObject)
        {
            return (anonymousObject != null) ? new RouteValueDictionary(anonymousObject) : HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
        }

        public static IDictionary<string, object> MergeObjects(this object htmlAttributesObject, object defaultHtmlAttributesObject)
        {
            var htmlAttributes = htmlAttributesObject.ToIDictionary();
            var defaultHtmlAttributes = defaultHtmlAttributesObject.ToIDictionary();

            foreach (var item in htmlAttributes)
            {
                if (defaultHtmlAttributes.ContainsKey(item.Key))
                    defaultHtmlAttributes[item.Key] = item.Value;
                else
                    defaultHtmlAttributes.Add(item.Key, item.Value);
            }

            return defaultHtmlAttributes;
        }
    }
}

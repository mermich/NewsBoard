using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.Tools
{
    public class NewsBoardUrlHelper
    {
        private IUrlHelper iUrlHelper;

        public NewsBoardUrlHelper(IUrlHelper iUrlHelper)
        {
            this.iUrlHelper = iUrlHelper;
        }

        public string Action(string area, string controller, string action, object values = null)
        {
            var areaObject = new { area = area };
            var merged = areaObject.MergeObjects(values);
            return iUrlHelper.Action(action, controller, merged);
        }
    }

    public static class UrlHelperExtensions
    {
        public static NewsBoardUrlHelper NewsBoardUrlHelper(this IUrlHelper iUrlHelper)
        {
            return new NewsBoardUrlHelper(iUrlHelper);
        }
    }
}

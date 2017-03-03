using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsBoard.Tools
{
    public static class NewsBoardUIHelperExtensions
    {
        public static NewsBoardUIHelper<TModel> NewsBoardUIHelper<TModel>(this IHtmlHelper<TModel> iUrlHelper)
        {
            return new NewsBoardUIHelper<TModel>(iUrlHelper);
        }
    }
}

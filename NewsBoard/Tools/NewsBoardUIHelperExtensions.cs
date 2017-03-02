using Microsoft.AspNetCore.Mvc.Rendering;
using NewBoardRestApi.BaseApi;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

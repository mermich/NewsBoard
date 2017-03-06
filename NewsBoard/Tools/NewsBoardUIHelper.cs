using Microsoft.AspNetCore.Mvc.Rendering;
using NewBoardRestApi.BaseApi;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsBoard.Tools
{
    public class NewsBoardUIHelper<TModel>
    {
        private IHtmlHelper<TModel> iHtmlHelper;


        public NewsBoardUIHelper(IHtmlHelper<TModel> iHtmlHelper)
        {
            this.iHtmlHelper = iHtmlHelper;
        }        
    }
}

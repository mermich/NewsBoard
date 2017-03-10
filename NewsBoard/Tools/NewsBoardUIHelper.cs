using Microsoft.AspNetCore.Mvc.Rendering;

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

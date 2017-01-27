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

        public string CheckBoxListFor(Expression<Func<TModel, SelectableItemList>> expr)
        {
            var defaultHtmlAttributesObject = new { data_label_width = "0", data_handle_width = "100", data_on_text = "Oui", data_off_text = "Non" };
            var sb = new StringBuilder();
            var selectableItemList = expr.Compile()(iHtmlHelper.ViewData.Model);

            var selectableItemListName = iHtmlHelper.NameFor(expr);
            for (int i = 0; i < selectableItemList.Items.Count(); i++)
            {
                var selectableItemListNameItemIdName = selectableItemListName + "[" + i + "].Id";
                var selectableItemListNameItemIdSelected = selectableItemListName + "[" + i + "].Selected";
                var selectableItemListNameItemIdValue = selectableItemListName + "[" + i + "].Value";

                sb.Append("<div class='checkbox'><label>");
                sb.Append($"<input type='hidden' name='{selectableItemListNameItemIdName}' value='{selectableItemList.Items[i].Id}' />");
                var isChecked = selectableItemList.Items[i].IsSelected ? "checked='checked'" : "";
                sb.Append($"<input type='checkbox' name='{selectableItemListNameItemIdSelected}' value='true' {isChecked} />");
                sb.Append(selectableItemList.Items[i].Label);
                sb.Append("</label></div>");
            }

            return sb.ToString();
        }
    }

    public static class NewsBoardUIHelperExtensions
    {
        public static NewsBoardUIHelper<TModel> NewsBoardUIHelper<TModel>(this IHtmlHelper<TModel> iUrlHelper)
        {
            return new NewsBoardUIHelper<TModel>(iUrlHelper);
        }
    }
}

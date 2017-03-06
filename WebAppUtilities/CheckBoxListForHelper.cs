using ApiUtilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WebAppUtilities
{
    public static class CheckBoxListForHelper
    {
        public static string CheckBoxListFor<TModel>(this IHtmlHelper<TModel> helper,  Expression<Func<TModel, SelectableItemList>> expr)
        {
            var defaultHtmlAttributesObject = new { data_label_width = "0", data_handle_width = "100", data_on_text = "Oui", data_off_text = "Non" };
            var sb = new StringBuilder();
            var selectableItemList = expr.Compile()(helper.ViewData.Model);

            var selectableItemListName = helper.NameFor(expr);
            for (int i = 0; i < selectableItemList.Items.Count(); i++)
            {
                var selectableItemListNameItemIdName = selectableItemListName + "[" + i + "].Id";
                var selectableItemListNameItemIdSelected = selectableItemListName + "[" + i + "].Selected";
                var selectableItemListNameItemIdValue = selectableItemListName + "[" + i + "].Value";

                sb.Append("<div class='checkbox'><label>");
                sb.Append($"<input type='hidden' name='{selectableItemListNameItemIdName}' value='{selectableItemList.Items[i].Value}' />");
                var isChecked = selectableItemList.Items[i].IsSelected ? "checked='checked'" : "";
                sb.Append($"<input type='checkbox' name='{selectableItemListNameItemIdSelected}' value='true' {isChecked} />");
                sb.Append(selectableItemList.Items[i].Label);
                sb.Append("</label></div>");
            }

            return sb.ToString();
        }
    }
}

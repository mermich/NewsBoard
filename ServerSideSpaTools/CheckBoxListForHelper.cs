using Microsoft.AspNetCore.Mvc.Rendering;
using ApiTools.Selectable;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ServerSideSpaTools
{
    public static class CheckBoxListForHelper
    {
        public static string CheckBoxListFor<TModel,U>(this IHtmlHelper<TModel> helper,  Expression<Func<TModel, SelectableItemList<U>>> expr)
            where U : IEquatable<U>
        {
            var defaultHtmlAttributesObject = new { data_label_width = "0", data_handle_width = "100", data_on_text = "Oui", data_off_text = "Non" };
            var sb = new StringBuilder();
            var selectableItemList = expr.Compile()(helper.ViewData.Model);

            var selectableItemListName = helper.NameFor(expr);
            for (int i = 0; i < selectableItemList.Items.Count(); i++)
            {
                var selectableItemListNameItemIdSelected = selectableItemListName + ".Items[" + i + "].IsSelected";
                var selectableItemListNameItemIdValue = selectableItemListName + ".Items[" + i + "].Value";

                sb.Append("<div class='checkbox'><label>");
                sb.Append($"<input type='hidden' name='{selectableItemListNameItemIdValue}' value='{selectableItemList.Items[i].Value}' />");

                var isChecked = selectableItemList.Items[i].IsSelected ? "checked='checked'" : "";
                sb.Append($"<input type='checkbox' name='{selectableItemListNameItemIdSelected}' value='true' {isChecked} />");
                sb.Append(selectableItemList.Items[i].Label);
                sb.Append("</label></div>");
            }

            return sb.ToString();
        }
    }
}

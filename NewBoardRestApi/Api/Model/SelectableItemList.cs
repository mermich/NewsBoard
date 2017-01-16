using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class SelectableItemList
    {
        public List<SelectableItem> Items { get; set; } = new List<SelectableItem>();

        public SelectableItemList() { }

        public SelectableItemList(IEnumerable<SelectableItem> items)
        {
            Items = items.ToList();
        }
    }


    public static class SelectableItemListExtentions
    {
        public static SelectableItemList ToSelectableItemList(this IEnumerable<Tag> tag, List<FeedTag> existingTags)
        {
            return new SelectableItemList(tag.Select(t => t.ToSelectableItem(existingTags)));
        }

        public static SelectableItemList ToSelectableItemList(this IEnumerable<Permission> permissions, List<GroupPermission> existingPermissions)
        {
            return new SelectableItemList(permissions.Select(t => t.ToSelectableItem(existingPermissions)));
        }
    }
}

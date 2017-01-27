using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.BaseApi
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


    internal static class SelectableItemListExtentions
    {
        internal static SelectableItemList ToSelectableItemList(this IEnumerable<Tag> tag, List<FeedTag> existingTags)
        {
            return new SelectableItemList(tag.Select(t => t.ToSelectableItem(existingTags)));
        }

        internal static SelectableItemList ToSelectableItemList(this IEnumerable<Permission> permissions, List<GroupPermission> existingPermissions)
        {
            return new SelectableItemList(permissions.Select(t => t.ToSelectableItem(existingPermissions)));
        }

        internal static SelectableItemList ToSelectableItemList(this IEnumerable<Group> groups, List<UserGroup> existingGroups)
        {
            return new SelectableItemList(groups.Select(t => t.ToSelectableItem(existingGroups)));
        }
    }
}

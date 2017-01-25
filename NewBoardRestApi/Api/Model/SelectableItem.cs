using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class SelectableItem
    {
        public int Id { get; set; }

        public bool IsSelected { get; set; }

        public string Label { get; set; }

        public SelectableItem()
        {
        }
    }

    internal static class SelectableItemExtentions
    {
        internal static SelectableItem ToSelectableItem(this Tag tag, List<FeedTag> existingTags)
        {
            return new SelectableItem
            {
                IsSelected = existingTags.Any(ft => ft.TagId == tag.Id),
                Label = tag.Label,
                Id = tag.Id
            };
        }

        internal static SelectableItem ToSelectableItem(this Permission permission, List<GroupPermission> existingPermissions)
        {
            return new SelectableItem
            {
                IsSelected = existingPermissions.Any(ft => ft.GroupId == permission.Id),
                Label = permission.Label,
                Id = permission.Id
            };
        }

        internal static SelectableItem ToSelectableItem(this Group group, List<UserGroup> existingGroups)
        {
            return new SelectableItem
            {
                IsSelected = existingGroups.Any(ft => ft.GroupId == group.Id),
                Label = group.Label,
                Id = group.Id
            };
        }
    }

}

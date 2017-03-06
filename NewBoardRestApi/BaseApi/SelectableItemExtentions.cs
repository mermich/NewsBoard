using ApiUtilities;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.BaseApi
{
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
                Value = permission.Id
            };
        }

        internal static SelectableItem ToSelectableItem(this Group group, List<UserGroup> existingGroups)
        {
            return new SelectableItem
            {
                IsSelected = existingGroups.Any(ft => ft.GroupId == group.Id),
                Label = group.Label,
                Value = group.Id
            };
        }
    }

}

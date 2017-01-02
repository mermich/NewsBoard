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

    public static class SelectableItemExtentions
    {
        public static SelectableItem ToSelectableItem(this Tag tag, List<FeedTag> existingTags)
        {
            return new SelectableItem
            {
                IsSelected = existingTags.Any(ft => ft.TagId == tag.Id),
                Label = tag.Label,
                Id = tag.Id
            };
        }
    }

}

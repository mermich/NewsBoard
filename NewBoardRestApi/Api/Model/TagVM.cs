﻿using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.Api.Model
{
    public class TagVM
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public int HitCount { get; set; }


        public TagVM()
        {
        }

        public TagVM(Tag tag, List< FeedTag> allTags)
        {
            Id = tag.Id;
            Label = tag.Label;
            HitCount = tag.FeedTags.Count;
        }
    }

    public static class TagVMExtentions
    {
        public static TagVM ToTag(this Tag item, List<FeedTag> allTags)
        {
            return new TagVM(item, allTags);
        }

        public static List<TagVM> ToTags(this List<Tag> items, List<FeedTag> allTags)
        {
            return items.Select(i => i.ToTag(allTags)).ToList();
        }
    }
}

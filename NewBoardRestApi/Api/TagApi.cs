using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.Api.Model;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.Api
{
    public class TagApi : BaseAuthenticatedApi
    {
        public TagApi(string userToken = "") : base(userToken)
        {
        }

        public TagVMList GetTags()
        {
            var feedTags = NewsBoardContext.FeedTags.ToList();

            return NewsBoardContext
                .Tags
                .Include(t => t.FeedTags)
                .ToTagVMList(feedTags);
        }

        public TagCreateVM GetNewCreateTag()
        {
            return new TagCreateVM();
        }
        

        public TagVM GetTag(int tagId)
        {
            var feedTags = NewsBoardContext.FeedTags.ToList();

            return NewsBoardContext
                .Tags
                .Include(t => t.FeedTags)
                .FirstOrDefault(t => t.Id == tagId)
                .ToTag(feedTags);
        }


        public TagEditVM GetEditTag(int tagId)
        {
            return NewsBoardContext
                .Tags
                .FirstOrDefault(t => t.Id == tagId)
                .ToTagEditVM();
        }

        public TagVM CreateTag(TagEditVM tagVM)
        {
            var tag = new Tag
            {
                Label = tagVM.Label
            };
            NewsBoardContext.Tags.Add(tag);
            NewsBoardContext.SaveChanges();

            return GetTag(tag.Id);
        }


        public TagVM SaveTag(TagEditVM tagVM)
        {
            var tag = NewsBoardContext.Tags.FirstOrDefault(t=>t.Id == tagVM.Id);
            tag.Label = tagVM.Label;

            NewsBoardContext.SaveChanges();

            return GetTag(tag.Id);
        }



        public FeedVMList GetFeedsForTag(int tagId)
        {
            return NewsBoardContext.Feeds.Where(f => f.FeedTags.Any(ft => ft.TagId == tagId)).ToFeedVMList();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.FeedApi;
using System.Linq;

namespace NewBoardRestApi.TagApi
{
    public class TagApi : BaseAuthenticatedApi
    {
        public TagApi(int userId) : base(userId)
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

        public TagVMList GetUsedTags()
        {
            var feedTags = NewsBoardContext.FeedTags.ToList();

            return NewsBoardContext
                .Tags
                .Include(t => t.FeedTags)
                .Where(t => t.FeedTags.Any())
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
            var tag = NewsBoardContext.Tags.FirstOrDefault(t => t.Id == tagVM.Id);
            tag.Label = tagVM.Label;

            NewsBoardContext.SaveChanges();

            return GetTag(tag.Id);
        }



        public FeedVMList GetFeedsForTag(int tagId)
        {
            return NewsBoardContext.Feeds
                .Include(f=>f.Articles).ThenInclude(article=>article.UserArticles)
                .Where(f => f.FeedTags.Any(ft => ft.TagId == tagId))
                .ToFeedVMList(UserId);
        }
    }
}

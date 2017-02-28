using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.TagApi
{
    public class TagVMList
    {
        public List<TagVM> Tags { get; set; }

        public TagVMList() { }

        public TagVMList(IEnumerable<TagVM> tags)
        {
            Tags = tags.ToList();
        }
    }
}

using System.Collections.Generic;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleListFilterVM
    {
        public List<int> Feeds { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 20;

        public bool OnlyUserSubscription { get; set; } = true;

        public bool HideReported { get; set; } = true;

        public string OrderBy { get; set; }

    }
}

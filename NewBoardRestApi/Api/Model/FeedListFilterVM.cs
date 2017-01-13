using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class FeedListFilterVM
    {
        public List<int> Tags { get; set; } = new List<int>();

        public int MaxItems { get; set; } = 20;

        public bool OnlyUserSubscription { get; set; } = true;

        public bool HideReported { get; set; } = true;

        public string OrderBy { get; set; }
    }
}

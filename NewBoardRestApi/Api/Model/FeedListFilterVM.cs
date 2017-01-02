using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.Api.Model
{
    public class FeedListFilterVM
    {
        public List<int> Tags { get; set; } = new List<int>();

        public string ToUrlQuery()
        {
            var result = "";



            result += string.Join("&", Tags.Select(t => $"?Tags={t}"));

            if (result.StartsWith("&"))
                result = "?" + result.Substring(1);

            return result;
        }
    }
}

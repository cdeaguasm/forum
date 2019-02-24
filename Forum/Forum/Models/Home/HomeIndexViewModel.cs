using Forum.Models.Post;
using System.Collections.Generic;

namespace Forum.Models.Home
{
    public class HomeIndexViewModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingViewModel> LatesPosts { get; set; }
    }
}

using Forum.Models.Post;
using System.Collections.Generic;

namespace Forum.Models.Home
{
    public class SearchResultViewModel
    {
        public IEnumerable<PostListingViewModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}

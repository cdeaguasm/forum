using Forum.Models.Post;
using System.Collections.Generic;

namespace Forum.Models.Forum
{
    public class ForumTopicViewModel
    {
        public ForumListingViewModel Forum { get; set; }
        public IEnumerable<PostListingViewModel> Posts { get; set; }
    }
}

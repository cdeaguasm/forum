using System;

namespace Forum.Models.Reply
{
    public class PostReplyViewModel
    {
        public int Id { get; set; }
        public string AuthroId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }

        public int PostId { get; set; }
    }
}

using Data.Interfaces;
using Data.Models;
using Forum.Models.Forum;
using Forum.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;

        public ForumController(IForumService forumService, IPostService postService)
        {
            _forumService = forumService;
            _postService = postService;
        }
        
        public ActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(f => new ForumListingViewModel
                {
                    Id = f.Id,
                    Name = f.Title,
                    Description = f.Description
                });

            var model = new ForumIndexViewModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        public ActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);
            var posts = new List<Post>();

            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();

            var postListings = posts.Select(p => new PostListingViewModel
            {
                Id = p.Id,
                AuthorId = p.User.Id,
                AuthorName = p.User.UserName,
                AuthorRating = p.User.Rating,
                Title = p.Title,
                DatePosted = p.Created.ToString(),
                RepliesCount = p.Replies.Count(),
                Forum = BuildForumListing(p)
            });

            var model = new ForumTopicViewModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }
        
        private ForumListingViewModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListingViewModel BuildForumListing(ForumE forum)
        {
            return new ForumListingViewModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}
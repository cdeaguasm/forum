using Data.Interfaces;
using Data.Models;
using Forum.Models.Forum;
using Forum.Models.Home;
using Forum.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Forum.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService _postService;

        public SearchController(IPostService postService)
        {
            _postService = postService;
        }
        
        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery);
            var noResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(p => new PostListingViewModel
            {
                Id = p.Id,
                AuthorId = p.User.Id,
                AuthorName = p.User.UserName,
                AuthorRating = p.User.Rating,
                Title = p.Title,
                DatePosted = p.Created.ToString(),
                RepliesCount = p.Replies.Count(),
                Forum = BuildingListing(p)
            });

            var model = new SearchResultViewModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResults = noResults
            };

            return View(model);
        }

        private ForumListingViewModel BuildingListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingViewModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Name = forum.Title,
                Description = forum.Description
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Data.Interfaces;
using Forum.Models.Home;
using Forum.Models.Post;
using Data.Models;
using Forum.Models.Forum;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildindHomeIndexViewModel();
            return View(model);
        }

        private HomeIndexViewModel BuildindHomeIndexViewModel()
        {
            var latestPosts = _postService.GetLatestPosts(10);
            var posts = latestPosts.Select(p => new PostListingViewModel
            {
                Id = p.Id,
                Title = p.Title,
                AuthorName = p.User.UserName,
                AuthorId = p.User.Id,
                AuthorRating = p.User.Rating,
                DatePosted = p.Created.ToString(),
                RepliesCount = p.Replies.Count(),
                Forum = GetForumListingForPost(p)
            });

            return new HomeIndexViewModel
            {
                SearchQuery = "",
                LatesPosts = posts
            };
        }

        private ForumListingViewModel GetForumListingForPost(Post p)
        {
            var forum = p.Forum;

            return new ForumListingViewModel
            {
                Id = forum.Id,
                Name = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

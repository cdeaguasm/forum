using Data.Interfaces;
using Data.Models;
using Forum.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Replies).ThenInclude(r => r.User)
                .Include(p => p.Forum);
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.User)
                .Include(p => p.Replies).ThenInclude(r => r.User)
                .Include(p => p.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPosts(ForumE forum, string searchQuery)
        {
            var _forum = _context.Forums.Find(forum.Id);
            return string.IsNullOrEmpty(searchQuery) 
                ? forum.Posts 
                : forum.Posts.Where(p 
                    => p.Title.Contains(searchQuery)
                    || p.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAll().OrderByDescending(p => p.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums
                .Where(f => f.Id == id)
                .First()
                .Posts;
        }
    }
}

﻿using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPostService
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(ForumE forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
    }
}

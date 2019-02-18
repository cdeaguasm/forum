using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IForumService
    {
        ForumE GetById(int id);
        IEnumerable<ForumE> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(ForumE forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
    }
}

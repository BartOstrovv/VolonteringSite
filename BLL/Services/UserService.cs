using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public void AddCommentToUser(string userID, Comment comment)
        {
               var USER =  _repo.FindByConditionAsync(x=>x.UserName == userID).Result;
            USER.First().Comments.Add(comment);
            _repo.Update(USER.First());
        }
    }
}

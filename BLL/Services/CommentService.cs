using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService
    {
        private readonly CommentRepository _repo;

        public CommentService(CommentRepository repo)
        {
            _repo = repo;
        }

    }
}

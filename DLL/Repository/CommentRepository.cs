using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;

namespace DLL.Repository
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(VolunteeringContext context) : base(context)
        {
        }

    }
}

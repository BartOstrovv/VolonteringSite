using DLL.Repository;
using Domain.Models;

namespace BLL.Services
{
    public class CommentService
    {
        private readonly CommentRepository _repo;

        public CommentService(CommentRepository repo)
        {
            _repo = repo;
        }

        public async Task<DLL.Models.OperationDetails> NewComment(string text, int adId, string userId)
        {
           return await  _repo.CreateAsync(new Domain.Models.Comment { Text = text, AdvertisementId = adId, UserId = userId });
        }

        public async Task<Comment> FindByText(string text)
        {
            return (await _repo.FindByConditionAsync(x => x.Text == text)).First();
        }
    }
}

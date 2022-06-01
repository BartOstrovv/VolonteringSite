using DLL.Repository;
using Domain.Models;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task UpdateAsync(User advertisement) => await _repo.Update(advertisement);
        public async Task<IReadOnlyCollection<User>> GetAllAsync() => await _repo.GetAllAsync();
    }
}

using DLL.Repository;
using Domain.Models;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _repoUser;
        private readonly PersonDataRepository _repoInfo;

        public UserService(UserRepository repo, PersonDataRepository personDataRepository)
        {
            _repoUser = repo;
            _repoInfo = personDataRepository;
        }

        public async Task UpdateAsync(User user) => await _repoUser.Update(user);
        public async Task<IReadOnlyCollection<User>> GetAllAsync() => await _repoUser.GetAllAsync();

        public async Task<User> FindUserAsync(string id) => (await _repoUser.FindByConditionAsync(x => String.Equals(id, x.Id))).First();

        public async Task UpdateInfo(PersonData data) => await _repoInfo.Update(data);
    }
}

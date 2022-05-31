using DLL.Repository;
using Domain.Models;
using DLL.Models;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class DonationService
    {
        private readonly DonationRepository _repo;
        private readonly UserRepository _repoUsers;

        public DonationService(DonationRepository repo, UserRepository userRepository)
        {
            _repo = repo;
            _repoUsers = userRepository;
        }

        public async Task<OperationDetails> NewDonat(int? adId, string userId, string coment, DateTime dateTime, double sum)
        {
            return await _repo.CreateAsync(new Donation() { AdvertisementId = adId, DateTime = dateTime, Sum = sum, UserId = userId, Comment = coment });
        }

        public async Task<IReadOnlyCollection<Donation>> GetFromAdAsync(int id)
        {
            var donations = await _repo.FindByConditionAsync(x => x.AdvertisementId == id).ConfigureAwait(false);
            foreach(var donation in donations) // display PersonData instead of UserID
            {
                var user = (await _repoUsers.FindByConditionAsync(x => x.Id == donation.UserId)).First();
                var data = $"{ user.PersonData?.Name} {user.PersonData?.Surname}";
                if (!String.IsNullOrEmpty(data))
                    donation.UserId = data;
            }
            return donations;
        }
    }
}

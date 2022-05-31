using DLL.Repository;
using Domain.Models;

namespace BLL.Services
{
    public class DonationService
    {
        private readonly DonationRepository _repo;

        public DonationService(DonationRepository repo)
        {
            _repo = repo;
        }

        public async Task<DLL.Models.OperationDetails> NewDonat(int? adId, string userId, string coment, DateTime dateTime, double sum)
        {
            return await _repo.CreateAsync(new Donation() { AdvertisementId = adId, DateTime = dateTime, Sum = sum, UserId = userId, Comment = coment });
        }
    }
}

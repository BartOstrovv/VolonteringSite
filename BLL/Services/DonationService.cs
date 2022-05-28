using DLL.Repository;
namespace BLL.Services
{
    public class DonationService
    {
        private readonly DonationRepository _repo;

        public DonationService(DonationRepository repo)
        {
            _repo = repo;
        }

    }
}

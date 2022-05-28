using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdvertisementService
    {
        private readonly AdvertisementRepository _repo;
        public AdvertisementService(AdvertisementRepository repo) => _repo = repo;

        public async Task<IReadOnlyCollection<Advertisement>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<IReadOnlyCollection<Advertisement>> GetAllClosedAsync() => await _repo.FindByConditionAsync(x => x.Close == true);

        public async void AddAdvertisementAsync(Advertisement advertisement)
        {
            await _repo.CreateAsync(advertisement);
        }

        public async Task<Advertisement> FindAdvertisementAsync(int id) => (await _repo.FindByConditionAsync(x => x.Id == id)).First();

        public void EditAddvertisement(Advertisement advertisement, int id)
        {
            var adv = _repo.FindByConditionAsync(x => x.Id == id).Result.First();
            if (adv != null)
            {
                adv = advertisement;
                _repo.Update(adv);
            }
        }

        public Advertisement AddCommentToAd(Comment comment, int adId)
        {
            var adv = _repo.FindByConditionAsync(x => x.Id == adId).Result.First();
            if (adv != null)
            {
                adv.Comments.Add(comment);
                _repo.Update(adv);
            }
            return adv;
        }

        public async Task<IReadOnlyCollection<Advertisement>> FindAds(string text) => (await _repo.FindByConditionAsync(x => x.Body.Contains(text) || x.Title.Contains(text)));
    }
       
}

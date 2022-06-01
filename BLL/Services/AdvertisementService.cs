using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdvertisementService
    {
        private readonly AdvertisementRepository _repo;
        public AdvertisementService(AdvertisementRepository repo) => _repo = repo;

        public async Task<IReadOnlyCollection<Advertisement>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task AddAdvertisementAsync(Advertisement advertisement)
        {
            await _repo.CreateAsync(advertisement);
        }

        public async Task<Advertisement> FindAdvertisementAsync(int id) => (await _repo.FindByConditionAsync(x => x.Id == id)).First();

        public async Task UpdateAsync(Advertisement advertisement) => await _repo.Update(advertisement);

        public async Task<IReadOnlyCollection<Advertisement>> FindByAdContentAsync(string text) => (await _repo.FindByConditionAsync(x => x.Body.Contains(text) || x.Title.Contains(text)));

        public async Task<IReadOnlyCollection<Advertisement>> GetAllByAsync(Expression<Func<Advertisement, bool>> predicat) => await _repo.FindByConditionAsync(predicat);
    }
       
}

using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public class AdvertisementRepository : BaseRepository<Advertisement>
    {
        public AdvertisementRepository(VolunteeringContext context) : base(context)
        {
        }
        public async override Task<IReadOnlyCollection<Advertisement>> FindByConditionAsync(Expression<Func<Advertisement, bool>> predicat)
        {
            return await Enntities.Include(ad=>ad.Comments).Include(p=>p.Images).Include(ad=>ad.DeliveryAddress).Include(ad=>ad.Donations).Where(predicat).ToListAsync().ConfigureAwait(false);
        }

        public async override Task<IReadOnlyCollection<Advertisement>> GetAllAsync()
        {
            return await Enntities.Include(ad => ad.Comments).AsSplitQuery().Include(p=>p.Images).Include(ad => ad.DeliveryAddress).Include(ad => ad.Donations).AsSplitQuery().ToListAsync().ConfigureAwait(false);
        }
    }
}

using DLL.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DLL.Repository.Interfaces;

namespace DLL.Repository
{
    public class DonationRepository : BaseRepository<Donation>
    {
        public DonationRepository(VolunteeringContext context) : base(context)
        {
        }

        public async override Task<IReadOnlyCollection<Donation>> FindByConditionAsync(Expression<Func<Donation, bool>> predicat)
        {
            return await Enntities.Where(predicat).ToListAsync().ConfigureAwait(false);
        }

        public async override Task<IReadOnlyCollection<Donation>> GetAllAsync()
        {
            return await Enntities.ToListAsync().ConfigureAwait(false);
        }
    }
}

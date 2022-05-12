using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class PersonDataRepository : BaseRepository<PersonData>
    {
        public PersonDataRepository(VolunteeringContext context) : base(context)
        {
        }

        public async override Task<IReadOnlyCollection<PersonData>> FindByConditionAsync(Expression<Func<PersonData, bool>> predicat)
        {
            return await Enntities.Include(p => p.Address).Include(p => p.Photo).Where(predicat).ToListAsync().ConfigureAwait(false);
        }

        public async override Task<IReadOnlyCollection<PersonData>> GetAllAsync()
        {
            return await Enntities.Include(p => p.Address).Include(p => p.Photo).ToListAsync().ConfigureAwait(false);
        }
    }
}

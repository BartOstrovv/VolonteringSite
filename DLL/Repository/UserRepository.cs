using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(VolunteeringContext context) : base(context)
        {
        }

        public async override Task<IReadOnlyCollection<User>> FindByConditionAsync(Expression<Func<User, bool>> predicat)
        {
            return await Enntities.Include(u => u.PersonData).Include(u => u.Advertisements).Include(u=>u.Comments).Include(u=>u.Donations).Where(predicat).ToListAsync().ConfigureAwait(false);
        }

        public async override Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await Enntities.Include(u => u.PersonData).Include(u => u.Advertisements).Include(u => u.Comments).Include(u => u.Donations).ToListAsync().ConfigureAwait(false);
        }
    }
}

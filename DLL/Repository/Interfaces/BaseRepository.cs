using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository.Interfaces
{
    public abstract class BaseRepository<TEnntity> : IRepository<TEnntity> where TEnntity : class
    {
        protected VolunteeringContext _context;
        private DbSet<TEnntity> _enntities;
        protected DbSet<TEnntity> Enntities => _enntities ??= _context.Set<TEnntity>();

        protected BaseRepository(VolunteeringContext context) => _context = context;

        public virtual async Task<IReadOnlyCollection<TEnntity>> FindByConditionAsync(Expression<Func<TEnntity, bool>> predicat) 
            => await Enntities.Where(predicat).ToListAsync().ConfigureAwait(false);
        public virtual async Task<IReadOnlyCollection<TEnntity>> GetAllAsync() => await Enntities.ToListAsync().ConfigureAwait(false);

        public async Task<OperationDetails> CreateAsync(TEnntity enntity)
        {
            try
            {
                await Enntities.AddAsync(enntity).ConfigureAwait(false);
                await _context.SaveChangesAsync();
                return new OperationDetails() { Message = "Created", IsSuccessful = true };
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Fatal exception on create");
                return new OperationDetails() { Message = "Failed to create Entity", IsSuccessful = false };
            }
        }

        public async Task Update(TEnntity enntity)
        {
            _context.Entry(enntity).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

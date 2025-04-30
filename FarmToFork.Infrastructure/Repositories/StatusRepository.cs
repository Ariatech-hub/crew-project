using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Status> AddAsync(Status entity)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(Status entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Status>> GetAllAsync(bool disableTracking = true)
        {

            return disableTracking
                ? await _context.Statuses.OrderBy(x => x.Id).ToListAsync()
                : await _context.Statuses.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public Task<IEnumerable<Status>> GetAsync(Expression<Func<Status, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Status> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Status entity)
        {
            throw new NotImplementedException();
        }
    }
}

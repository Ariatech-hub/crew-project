using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Core.SqliteEntities;
using FarmToFork.Infrastructure.Context;

namespace FarmToFork.Infrastructure.Repositories
{
    public class SqlLiteRepository : ISqlLiteRepository
    {
        private readonly SqlLiteDbContext _dbContext;

        public SqlLiteRepository(SqlLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task InsertResponse(string data, string filed)
        {
            await _dbContext.Responses.AddAsync(new Response() {Data = data, Type = filed});
            await _dbContext.SaveChangesAsync();
        }
    }
}

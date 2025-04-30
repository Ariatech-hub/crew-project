using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Core.SqliteEntities;
using SQLitePCL;

namespace FarmToFork.Infrastructure.Context
{
    public class SqlLiteDbContext : DbContext
    {
        public SqlLiteDbContext()
        {
        }

        public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options)
            : base(options)
        {
        }


        public DbSet<Response> Responses { get; set; }

    }
}

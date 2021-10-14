using LmjHotelWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LmjHotelWebApplication.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        public DbSet<Hospede> Hospede { get; set; }
    }
}

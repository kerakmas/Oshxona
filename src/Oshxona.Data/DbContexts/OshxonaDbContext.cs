using Microsoft.EntityFrameworkCore;
using Oshxona.Domain.Commons;
using Oshxona.Domain.Entites;

namespace Oshxona.Data.DbContexts
{
    public class OshxonaDbContext : DbContext
    {
        public OshxonaDbContext(DbContextOptions<OshxonaDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Meal> Meals { get; set; }  


    }
}

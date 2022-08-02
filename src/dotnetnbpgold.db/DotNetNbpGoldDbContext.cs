using dotnetnbpgold.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetnbpgold.db
{
    public class DotNetNbpGoldDbContext : DbContext
    {
        public DotNetNbpGoldDbContext(DbContextOptions<DotNetNbpGoldDbContext> options)
            : base(options)
        {
        }
 
        public DbSet<GoldPrice> GoldPrices { get; set; }
    }
}
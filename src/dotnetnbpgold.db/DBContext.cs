using dotnetnbpgold.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnetnbpgold.db
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
 
        public DbSet<GoldPrice> GoldPrices { get; set; }
    }
}
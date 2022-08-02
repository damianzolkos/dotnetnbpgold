using dotnetnbpgold.db.Entities;

namespace dotnetnbpgold.db.Repositories
{
    public class GoldPriceRepository : GenericRepository<GoldPrice>, IGoldPriceRepository
    {
        public GoldPriceRepository(DotNetNbpGoldDbContext context) : base(context)
        {
        }
    }
}
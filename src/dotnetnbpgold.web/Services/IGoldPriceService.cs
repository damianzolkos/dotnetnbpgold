using dotnetnbpgold.web.Models.ViewModels;

namespace dotnetnbpgold.web.Services
{
    public interface IGoldPriceService
    {
        Task<GoldPriceViewModel> GetForViewAsync(DateTime startDate, DateTime endDate);
        Task<IList<GoldPriceDBViewModel>> GetForListViewAsync();
    }
}
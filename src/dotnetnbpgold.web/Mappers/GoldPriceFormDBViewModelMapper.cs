using dotnetnbpgold.db.Entities;
using dotnetnbpgold.web.Models.ViewModels;

namespace dotnetnbpgold.web.Mappers
{
    public static class GoldPriceFormDBViewModelMapper
    {
        public static GoldPriceFormDBViewModel MapToGoldPriceDBViewModel(this GoldPrice goldPrice)
        {
            return new()
            { 
                StartDate = goldPrice.StartDate.Date,
                EndDate = goldPrice.EndDate.Date,
                Average = goldPrice.Average,
                AddedAt = goldPrice.AddedAt
            };
        } 
    }
}
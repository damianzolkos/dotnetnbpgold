using dotnetnbpgold.web.Models.DTOs;

namespace dotnetnbpgold.web.Models.ViewModels
{
    public class GoldPriceViewModel : AbstractViewModel
    {
        public DatePriceDTO StartDateGoldPrice { get; set; }
        public DatePriceDTO EndDateGoldPrice { get; set; }
        public decimal Average { get; set; }
    }
}
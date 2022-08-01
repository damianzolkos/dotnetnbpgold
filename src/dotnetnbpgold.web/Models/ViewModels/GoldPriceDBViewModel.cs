namespace dotnetnbpgold.web.Models.ViewModels
{
    public class GoldPriceDBViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Average { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
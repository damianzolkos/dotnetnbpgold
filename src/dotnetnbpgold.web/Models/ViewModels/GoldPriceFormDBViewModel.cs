namespace dotnetnbpgold.web.Models.ViewModels
{
    public class GoldPriceFormDBViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Average { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
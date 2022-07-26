namespace dotnetnbpgold.db.Entities
{
    public class GoldPrice : Entity, IAddable
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Average { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
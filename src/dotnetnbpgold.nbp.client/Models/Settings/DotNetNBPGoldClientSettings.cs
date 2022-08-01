namespace dotnetnbpgold.nbp.client.Models.Settings
{
    public class DotNetNBPGoldClientSettings
    {
        public string ApiUrl { get; set; } = "http://api.nbp.pl/api/cenyzlota/";
        public int MaxDaysPeriod { get; set; } = 93;
        public DateTime MinDate { get; set; } = new DateTime(2013, 1, 2);
    }
}
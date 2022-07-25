using System.Text.Json.Serialization;

namespace dotnetnbpgold.nbp.client.Models.NBP.Responses
{
    public class NBPGoldDatePriceResponse
    {
        [JsonPropertyName("data")]
        public string Date { get; set; }

        [JsonPropertyName("cena")]
        public decimal Price { get; set; }
    }
}
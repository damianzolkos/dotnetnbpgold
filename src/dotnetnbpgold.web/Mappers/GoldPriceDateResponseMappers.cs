using dotnetnbpgold.web.Models.DTOs;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;

namespace dotnetnbpgold.web.Mappers
{
    public static class GoldPriceDateResponseMappers
    {
        public static DatePriceDTO MapToDatePriceDTO(this NBPGoldDatePriceResponse response)
        {
            return new DatePriceDTO() { 
                Date = DateTime.Parse(response.Date),
                Price = response.Price
            };
        }
    }
}
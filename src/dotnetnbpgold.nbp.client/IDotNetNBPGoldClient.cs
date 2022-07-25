using dotnetnbpgold.nbp.client.Models.NBP.Responses;

namespace dotnetnbpgold.nbp.client
{
    public interface IDotNetNBPGoldClient
    {
        Task<List<NBPGoldDatePriceResponse>> GetGoldPricesAsync(DateTime startDate, DateTime endDate);
    }
}
using dotnetnbpgold.nbp.client.Exceptions;
using dotnetnbpgold.nbp.client.Helpers;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using dotnetnbpgold.nbp.client.Settings;

using Microsoft.Extensions.Options;

namespace dotnetnbpgold.nbp.client
{
    public class DotNetNBPGoldClient : IDotNetNBPGoldClient
    {
        private readonly DotNetNBPGoldClientSettings _settings;

        public DotNetNBPGoldClient(IOptions<DotNetNBPGoldClientSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<List<NBPGoldDatePriceResponse>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate < new DateTime(2013, 1, 2))
            {
                throw new DotNetNBPGoldClientException(""); // TODO: add propper exception
            }

            if ((endDate - startDate).Days > 93)
            {
                throw new DotNetNBPGoldClientException(""); // TODO: add propper exception
            }

            string url = GetGetGoldPricesUrl(startDate, endDate);
            List<NBPGoldDatePriceResponse> goldPrices = await HttpHelpers.HttpGetAsync<List<NBPGoldDatePriceResponse>>(url);

            return goldPrices;
        }

        private string GetGetGoldPricesUrl(DateTime startDate, DateTime endDate)
        {
            return $"{_settings.ApiUrl}{ParseDate(startDate)}/{ParseDate(endDate)}?format=json";
        }

        // TODO: move this to DateTime extension method
        private string ParseDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }      
}

using dotnetnbpgold.nbp.client.Exceptions;
using dotnetnbpgold.nbp.client.Helpers;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using dotnetnbpgold.nbp.client.Settings;

namespace dotnetnbpgold.nbp.client
{
    public class DotNetNBPGoldClient : IDotNetNBPGoldClient
    {
        private readonly DotNetNBPGoldClientSettings _settings;

        public DotNetNBPGoldClient(DotNetNBPGoldClientSettings settings)
        {
            _settings = settings;
        }

        public async Task<List<NBPGoldDatePriceResponse>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate < new DateTime(2013, 1, 2)) {
                throw new DotNetNBPGoldClientException(""); // TODO: add propper exception
            }

            if ((endDate - startDate).Days > 93) {
                throw new DotNetNBPGoldClientException(""); // TODO: add propper exception
            }

            string url = GetGetGoldPricesUrl(startDate, endDate);
            List<NBPGoldDatePriceResponse>? goldPrices = await HttpHelpers.HttpGetAsync<List<NBPGoldDatePriceResponse>>(url);

            if (goldPrices is null)
            {
                return new();
            }
            return goldPrices;
        }

        private string GetGetGoldPricesUrl(DateTime startDate, DateTime endDate)
        {
            return $"{_settings.ApiUrl}{startDate}/{endDate}";
        }
    }      
}

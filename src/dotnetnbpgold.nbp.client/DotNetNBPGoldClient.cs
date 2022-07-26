using dotnetnbpgold.nbp.client.Exceptions;
using dotnetnbpgold.nbp.client.Extensions;
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
            if (startDate.Date > DateTime.Now.Date)
            {
                throw new DotNetNBPGoldClientException("Start date cannot be in the future.");
            }

            if (endDate.Date > DateTime.Now.Date)
            {
                throw new DotNetNBPGoldClientException("End date cannot be in the future.");
            }

            if (endDate.Date < startDate.Date) {
                throw new DotNetNBPGoldClientException("End date cannot be after start date.");
            }

            if (startDate < new DateTime(2013, 1, 2))
            {
                throw new DotNetNBPGoldClientException("Start date cannot be before 1st of January 2013");
            }

            if ((endDate - startDate).Days > 93)
            {
                throw new DotNetNBPGoldClientException($"Maximum period is 93 days, you selected {(endDate - startDate).Days} days");
            }

            string url = GetGetGoldPricesUrl(startDate, endDate);
            List<NBPGoldDatePriceResponse> goldPrices = await HttpHelpers.HttpGetAsync<List<NBPGoldDatePriceResponse>>(url);

            return goldPrices;
        }

        private string GetGetGoldPricesUrl(DateTime startDate, DateTime endDate)
        {
            return $"{_settings.ApiUrl}{startDate.ParseDate()}/{endDate.ParseDate()}?format=json";
        }
    }      
}

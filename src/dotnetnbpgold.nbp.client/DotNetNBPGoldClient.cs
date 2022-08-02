using dotnetnbpgold.nbp.client.Exceptions;
using dotnetnbpgold.nbp.client.Extensions;
using dotnetnbpgold.nbp.client.Helpers;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using dotnetnbpgold.nbp.client.Models.Settings;
using Microsoft.Extensions.Options;

namespace dotnetnbpgold.nbp.client
{
    public class DotNetNBPGoldClient : IDotNetNBPGoldClient
    {
        private readonly DotNetNBPGoldClientSettings _settings;

        public DotNetNBPGoldClient(
            IOptions<DotNetNBPGoldClientSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<List<NBPGoldDatePriceResponse>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            ValidateRequest(startDate, endDate);
            string url = GetGetGoldPricesUrl(startDate, endDate);
            List<NBPGoldDatePriceResponse> goldPrices = await HttpHelpers.HttpGetAsync<List<NBPGoldDatePriceResponse>>(url);
            return goldPrices;
        }

        private void ValidateRequest(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date > DateTime.Now.Date)
            {
                throw new DotNetNBPGoldClientException("The start date cannot be in the future.");
            }

            if (endDate.Date > DateTime.Now.Date)
            {
                throw new DotNetNBPGoldClientException("The end date cannot be in the future.");
            }

            if (endDate.Date < startDate.Date)
            {
                throw new DotNetNBPGoldClientException("The end date cannot be before the start date.");
            }

            if (startDate.Date < _settings.MinDate)
            {
                throw new DotNetNBPGoldClientException("The start date cannot be before the 1st of January 2013");
            }

            int days = (endDate - startDate).Days;
            if (days > _settings.MaxDaysPeriod)
            {
                throw new DotNetNBPGoldClientException($"The maximum period is 93 days, you selected {days} days.");
            }
        }

        private string GetGetGoldPricesUrl(DateTime startDate, DateTime endDate)
        {
            return $"{_settings.ApiUrl}{startDate.ParseDate()}/{endDate.ParseDate()}?format=json";
        }
    }      
}

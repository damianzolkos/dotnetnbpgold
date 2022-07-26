using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using dotnetnbpgold.nbp.client;
using dotnetnbpgold.web.Models.DTOs;
using dotnetnbpgold.web.Mappers;
using dotnetnbpgold.web.Models.ViewModels;

namespace dotnetnbpgold.web.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        private readonly IDotNetNBPGoldClient _nbpClient;

        public GoldPriceService(IDotNetNBPGoldClient nbpClient)
        {
            _nbpClient = nbpClient;
        }

        private async Task<IList<DatePriceDTO>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            var prices = await _nbpClient.GetGoldPricesAsync(startDate, endDate);
            return prices.Select(p => p.MapToDatePriceDTO()).ToList();
        }

        public async Task<GoldPriceViewModel> GetForViewAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var prices = await GetGoldPricesAsync(startDate, endDate);
                
                // TODO: add null check and devision by zero check.
                var startDateGoldPrice = prices.FirstOrDefault();
                var endDateGoldPrice = prices.LastOrDefault();
                var average = Math.Round(prices.Sum(x => x.Price) / prices.Count, 2);

                return new() {
                    StartDateGoldPrice = startDateGoldPrice,
                    EndDateGoldPrice = endDateGoldPrice,
                    Average = average
                };
            }
            catch (Exception e)
            {
                return new() { ErrorMessage = e.Message };
            }
        }
    }
}
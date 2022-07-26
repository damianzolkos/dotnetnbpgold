using dotnetnbpgold.nbp.client;
using dotnetnbpgold.web.Models.DTOs;
using dotnetnbpgold.web.Mappers;
using dotnetnbpgold.web.Models.ViewModels;
using dotnetnbpgold.db.Repositories;
using dotnetnbpgold.db.Entities;

namespace dotnetnbpgold.web.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        private readonly IDotNetNBPGoldClient _nbpClient;
        private readonly IGoldPriceRepository _repository;

        public GoldPriceService(IDotNetNBPGoldClient nbpClient, IGoldPriceRepository repository)
        {
            _nbpClient = nbpClient;
            _repository = repository;
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

                await AddToDatebaseAsync(startDate, endDate, average);

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

        private async Task AddToDatebaseAsync(DateTime startDate, DateTime endDate, decimal average)
        {
            var goldPriceDbModel = new GoldPrice()
            {
                StartDate = startDate,
                EndDate = endDate,
                Average = average,
                AddedAt = DateTime.Now
            };

            await _repository.AddAsync(goldPriceDbModel);
        }
    }
}
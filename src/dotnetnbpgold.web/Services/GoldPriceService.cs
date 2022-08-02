using dotnetnbpgold.nbp.client;
using dotnetnbpgold.web.Models.DTOs;
using dotnetnbpgold.web.Mappers;
using dotnetnbpgold.web.Models.ViewModels;
using dotnetnbpgold.db.Repositories;
using dotnetnbpgold.db.Entities;
using System.Text.Json;
using System.Globalization;
using dotnetnbpgold.web.Models;

namespace dotnetnbpgold.web.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        private readonly IDotNetNBPGoldClient _nbpClient;
        private readonly IGoldPriceRepository _repository;
        private readonly IFileService _fileService;
        private readonly ILogger<GoldPriceService> _logger;

        public GoldPriceService(
            IDotNetNBPGoldClient nbpClient,
            IGoldPriceRepository repository,
            IFileService fileService,
            ILogger<GoldPriceService> logger)
        {
            _nbpClient = nbpClient;
            _repository = repository;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<GoldPriceViewModel> GetForViewAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var prices = await GetGoldPricesAsync(startDate, endDate);

                if (prices.Count == 0)
                {
                    var message = "None gold prices for selected period were find.";
                    _logger.LogDebug(message);
                    return new() { ErrorMessage = message };
                }

                var startDateGoldPrice = prices.FirstOrDefault();
                var endDateGoldPrice = prices.LastOrDefault();

                var average = Math.Round(prices.Sum(x => x.Price) / prices.Count, 2);

                await AddToDatebaseAsync(startDate, endDate, average);
                await AddToFileSystemAsync(startDate, endDate, average);

                _logger.LogInformation("Gold prices retrieved. Start date: {startDate}, end date: {endDate}, average: {average}", startDate, endDate, average);
                return new() {
                    StartDateGoldPrice = startDateGoldPrice,
                    EndDateGoldPrice = endDateGoldPrice,
                    Average = average
                };
            }
            catch (Exception e)
            {
                _logger.LogWarning("Something went wrong while getting gold prices. Exception: {{exceptionMessage}}", e.Message);
                return new() { ErrorMessage = e.Message };
            }
        }

        private async Task<IList<DatePriceDTO>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            var prices = await _nbpClient.GetGoldPricesAsync(startDate, endDate);
            return prices.Select(p => p.MapToDatePriceDTO()).ToList();
        }

        public async Task<IList<GoldPriceDBViewModel>> GetForListViewAsync() {
            var goldPricesFromDB = await _repository.GetAllAsync();
            List<GoldPriceDBViewModel> goldPriceDBViewModels = goldPricesFromDB.Select(p => p.MapToGoldPriceDBViewModel()).ToList();
            _logger.LogInformation("Gold prices retrieved form DB.");
            return goldPriceDBViewModels;
        }

        private async Task AddToDatebaseAsync(DateTime startDate, DateTime endDate, decimal average)
        {
            var goldPriceDbModel = new GoldPrice()
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Average = average,
                AddedAt = DateTime.Now
            };

            await _repository.AddAsync(goldPriceDbModel);
            _logger.LogInformation("Succesfully saved to database, with ID: {id}", goldPriceDbModel.Id);
        }

        private async Task AddToFileSystemAsync(DateTime startDate, DateTime endDate, decimal average)
        {
            var goldPriceFileModel = new GoldPriceFileModel()
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Average = average
            };

            var goldPriceModelJsonString = JsonSerializer.Serialize(goldPriceFileModel);
            await _fileService.SaveTextFileAsync(GetDirectoryName(), GetFileName(), goldPriceModelJsonString);
            _logger.LogInformation("Succesfully saved to file.");
        }

        private string GetDirectoryName() {
            return DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private string GetFileName() {
            return DateTime.Now.ToString("hh-mm-ss", CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString("N")[..6] + ".json";
        }
    }
}
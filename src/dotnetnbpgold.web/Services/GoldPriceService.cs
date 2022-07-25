using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using dotnetnbpgold.nbp.client;

namespace dotnetnbpgold.web.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        private readonly IDotNetNBPGoldClient _nbpClient;

        public GoldPriceService(IDotNetNBPGoldClient nbpClient)
        {
            _nbpClient = nbpClient;
        }

        public async Task<string> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            var prices = await _nbpClient.GetGoldPricesAsync(startDate, endDate);
            return JsonSerializer.Serialize(prices); 
        }
    }
}
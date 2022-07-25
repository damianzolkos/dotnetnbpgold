using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetnbpgold.web.Services
{
    public interface IGoldPriceService
    {
        Task<string> GetGoldPricesAsync(DateTime startDate, DateTime endDate);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetnbpgold.web.Models.DTOs;
using dotnetnbpgold.web.Models.ViewModels;

namespace dotnetnbpgold.web.Services
{
    public interface IGoldPriceService
    {
        Task<GoldPriceViewModel> GetForViewAsync(DateTime startDate, DateTime endDate);
        Task<IList<GoldPriceDBViewModel>> GetForListViewAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetnbpgold.web.Models.DTOs;

namespace dotnetnbpgold.web.Models.ViewModels
{
    public class GoldPriceViewModel : AbstractViewModel
    {
        public DatePriceDTO StartDateGoldPrice { get; set; }
        public DatePriceDTO EndDateGoldPrice { get; set; }
        public decimal Average { get; set; }
    }
}
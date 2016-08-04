using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProducePricingFrontEnd.Models
{
    public class SearchViewModel
    {

    }

    public class indexSearchVM
    {
        public decimal? lowPriceMin { get; set; }
        public decimal? highPriceMax { get; set; }
        public string commName { get; set; }
        public string packageDesc { get; set; }
        public string itemSizeDesc { get; set; }
        public string varietyName { get; set; }
        public string reportDate { get; set; }
        public int tMarketID { get; set; }
        public double? lastWeekLowPriceMin { get; set; }
        public double? lastWeekHighPriceMax { get; set; }
        public double? lastWeekAvg { get; set; }
    }
}
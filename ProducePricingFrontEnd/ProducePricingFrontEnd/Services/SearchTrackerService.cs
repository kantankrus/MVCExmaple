using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ProducePricingFrontEnd.Models;

namespace ProducePricingFrontEnd.Services
{
    public class SearchTrackerService
    {
        private DB_9F3E71_ppEntities db = new DB_9F3E71_ppEntities();

        public void saveUserSearch(string ip_address, string comm_name, 
                                   string package_desc, string item_size_desc, 
                                   string variety_name, string market_name)
        {
            DateTime dtNow = DateTime.Now;

            searchTracker st = new searchTracker
            {
                ipAddress = ip_address,
                commName = comm_name,
                packageDesc = package_desc,
                itemSizeDesc = item_size_desc,
                varietyName = variety_name,
                marketName = market_name,
                searchDate = dtNow
            };

            db.searchTrackers.Add(st);
            db.SaveChanges();
        }
    }
}
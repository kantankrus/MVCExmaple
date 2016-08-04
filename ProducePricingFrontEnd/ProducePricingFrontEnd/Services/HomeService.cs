using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProducePricingFrontEnd.Models;

namespace ProducePricingFrontEnd.Services
{
    public class HomeService
    {
        private DB_9F3E71_ppEntities db = new DB_9F3E71_ppEntities();

        public IEnumerable<SelectListItem> getAvailableMarketsDropDown()
        {
            return new[] { new SelectListItem { Value = "[Select Market]", Text = "[Select Market]" } }
                                      .Concat(db.tMarketTables
                                      .Select(x => new SelectListItem { Value = x.cityName, Text = x.cityName })
                                      .Distinct()
                                      .OrderBy(x => x.Value)
                                      .ToList());
        }

        public IEnumerable<object> getProducePossibilities(string prod_term, string market_name) {
            var result = db.tMarketTables.Where(x => x.commName.Contains(prod_term.ToUpper()) || x.itemSizeDesc.Contains(prod_term.ToUpper()) || x.varietyName.Contains(prod_term.ToUpper()) || x.packageDesc.Contains(prod_term.ToUpper()))
                                         .Where(x => x.cityName == market_name)
                                         .Select(x => new
                                         { id = x.commName + " " + x.packageDesc + " " + x.itemSizeDesc + " " + x.varietyName,
                                           value = x.commName + " " + x.packageDesc + " " + x.itemSizeDesc + " " + x.varietyName,
                                           commName = x.commName,
                                           packageDesc = x.packageDesc,
                                           itemSizeDesc = x.itemSizeDesc,
                                           varietyName = x.varietyName
                                         })
                                         .Distinct()
                                         .Take(20)
                                         .ToList();
            return result;
        }

        public object getProducePricing(string strCommName, string strPackageDesc, string strItemSizeDesc, string strVarietyName, string strMarketName)
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            var query = (from t in db.tMarketTables
                            .Select(t => t)
                            .Where(t => t.cityName == strMarketName)
                            .OrderByDescending(x => x.reportDate)
                         select t);

            var query1 = (from t in db.tMarketTables
                            .Select(t => t)
                            .Where(t => t.cityName == strMarketName)
                            .Where(t => t.reportDate == dateCriteria)
                            .OrderByDescending(t => t.reportDate)
                          select t);

            if (!string.IsNullOrWhiteSpace(strCommName))
            {
                query = query.Where(t => t.commName == strCommName.ToUpper());
                query1 = query1.Where(t => t.commName == strCommName.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(strPackageDesc))
            {
                query = query.Where(t => t.packageDesc == strPackageDesc.ToUpper());
                query1 = query1.Where(t => t.packageDesc == strPackageDesc.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(strItemSizeDesc))
            {
                query = query.Where(t => t.itemSizeDesc == strItemSizeDesc.ToUpper());
                query1 = query1.Where(t => t.itemSizeDesc == strItemSizeDesc.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(strVarietyName))
            {
                query = query.Where(t => t.varietyName == strVarietyName.ToUpper());
                query1 = query1.Where(t => t.varietyName == strVarietyName.ToUpper());
            }
            
            var result = query.Select(x => new indexSearchVM
            {
                lowPriceMin = x.lowPriceMin,
                highPriceMax = x.highPriceMax,
                commName = x.commName,
                packageDesc = x.packageDesc,
                itemSizeDesc = x.itemSizeDesc,
                varietyName = x.varietyName,
                reportDate = x.reportDate.ToString(),
                tMarketID = x.tMarketID,
                lastWeekAvg = (from t in query1 select (Math.Round((double)t.highPriceMax + (double)t.lowPriceMin)/2)).FirstOrDefault()
            })
            .FirstOrDefault();

            return result;
        }
    }
}
 
 
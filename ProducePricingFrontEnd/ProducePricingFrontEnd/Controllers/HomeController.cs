using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProducePricingFrontEnd.Services;
using ProducePricingFrontEnd.Models;
using System.Web.Mvc;

namespace ProducePricingFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        HomeService hs = new HomeService();
        SearchTrackerService sts = new SearchTrackerService();
        MainViewModel mvm = new MainViewModel();
        public ActionResult Index()
        {
            ViewBag.Title = "Producepricing.net Terminal Market Data Everyday";
            mvm.Markets = hs.getAvailableMarketsDropDown();
            return View(mvm);
        }

        //Method for receiving AJAX post from Home/index. Gathers user search string and market name.
        public JsonResult GetProduceItems(string prod_term, string market_name)
        {
            return Json(hs.getProducePossibilities(prod_term, market_name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketDataUserSelection(string commName, string packageDesc, string itemSizeDesc, string varietyName, string market_name)
        {
            
            return Json(hs.getProducePricing(commName, packageDesc, itemSizeDesc, varietyName, market_name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserSearch(string commName, string packageDesc, 
                                        string itemSizeDesc, string varietyName, 
                                        string marketName)
        {
            sts.saveUserSearch(System.Web.HttpContext.Current.Request.UserHostAddress,
                               commName, packageDesc, itemSizeDesc, varietyName, marketName);
            return Json( new {result =  "Correctly saved user info." });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProducePricingFrontEnd.Models
{
    public class MainViewModel
    {
        public IEnumerable<SelectListItem> Markets { get; set; }
    }
}
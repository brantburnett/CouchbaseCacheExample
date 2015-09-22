using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CouchbaseCache.Classes;

namespace CouchbaseCache.Controllers
{
    public class HomeController : Controller
    {

        private readonly WebOptions _options;

        public HomeController(WebOptions options)
        {
            _options = options;
        }

        public ActionResult Index()
        {
            return View(_options);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MobileTVGuide.Models;
using MobileTVGuide.Services.TvGuides;

namespace MobileTVGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ITvGuideService tvGuide = new TvRage();
            Guide guide = tvGuide.Retrieve();

            return View(guide);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

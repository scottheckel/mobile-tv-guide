using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileTVGuide.Models;
using MobileTVGuide.Services.TvGuides;

namespace MobileTVGuide.Controllers
{
    public class GuideController : Controller
    {
        //
        // GET: /Guide/

        public ActionResult Index()
        {
            ITvGuideService tvGuide = new TvRage();
            Guide guide = tvGuide.Retrieve(true);
            return View(guide);
        }

        public ActionResult Timeslot(int? startHour)
        {
            ITvGuideService tvGuide = new TvRage();
            Guide guide = tvGuide.Retrieve(false);
            return View(guide);
        }

    }
}

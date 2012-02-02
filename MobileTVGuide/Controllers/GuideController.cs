using System.Web.Mvc;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.TvGuides;

namespace MobileTVGuide.Controllers
{
    public class GuideController : Controller
    {
        private ITvGuideService guideService;

        public GuideController(ITvGuideService guideService)
        {
            this.guideService = guideService;
        }

        public ActionResult Index()
        {
            Guide guide = guideService.Retrieve(true);
            return View(guide);
        }

        public ActionResult Timeslot(int? startHour)
        {
            Guide guide = guideService.Retrieve(false);
            return View(guide);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MobileTVGuide.Models;

namespace MobileTVGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // TODO: Quick and very dirty

            WebClient webClient = new WebClient();
            var schedule = webClient.DownloadString("http://services.tvrage.com/tools/quickschedule.php");

            string[] lines = schedule.Split('\n');
            Guide guide = new Guide();

            string day = null;
            string time = null;
            bool isToday = false;
            foreach (var line in lines)
            {
                if(line.StartsWith("[DAY]"))
                {
                    day = line.Substring(5, line.Length - 11);

                    isToday = DateTime.Parse(day) == DateTime.Today;
                }
                else if(isToday)
                {
                    if (line.StartsWith("[SHOW]"))
                    {
                        var temp = line.Substring(6, line.Length - 13);
                        var showInfo = temp.Split('^');

                        if(!guide.Channels.ContainsKey(showInfo[0]))
                        {
                            guide.Channels.Add(showInfo[0], new Channel(showInfo[0]));
                        }

                        guide.Channels[showInfo[0]].Shows.Add(new Show
                        {
                            Name = showInfo[1],
                            StartTime = time
                        });
                    }
                    else if (line.StartsWith("[TIME]"))
                    {
                        time = line.Substring(6, line.Length - 13);
                    }
                }

            }

            return View(guide);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

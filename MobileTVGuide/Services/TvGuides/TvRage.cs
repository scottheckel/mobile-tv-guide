using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileTVGuide.Models;
using System.Net;
using MobileTVGuide.Helpers;

namespace MobileTVGuide.Services.TvGuides
{
    /// <summary>Retrieve Tv Guide information from Tv Rage</summary>
    public class TvRage : ITvGuideService
    {
        /// <summary>Retrieve the Schedule form Tv Rage</summary>
        /// <returns>Populated Guide</returns>
        public Guide Retrieve()
        {
            // TODO: Quick and very dirty

            // Create the web client and download the request
            WebClient webClient = new WebClient();
            string schedule = webClient.DownloadString("http://services.tvrage.com/tools/quickschedule.php");

            string[] lines = schedule.Split('\n');
            Guide guide = new Guide();

            string day = null;

            DateTime today = EasternTimeZone.Today;
            DateTimeOffset time = today;
            bool isToday = false;
            foreach (var line in lines)
            {
                if (line.StartsWith("[DAY]"))
                {
                    day = line.Substring(5, line.Length - 11);
                    isToday = EasternTimeZone.Parse(day, DateTimeOffset.MinValue) == today;
                }
                else if (isToday)
                {
                    if (line.StartsWith("[SHOW]"))
                    {
                        ParseShow(guide, time, line);
                    }
                    else if (line.StartsWith("[TIME]"))
                    {
                        var tempTime = line.Substring(6, line.Length - 13);
                        time = EasternTimeZone.Parse(tempTime, today);
                    }
                }
            }

            return guide;
        }

        private static void ParseShow(Guide guide, DateTimeOffset time, string line)
        {
            var temp = line.Substring(6, line.Length - 13);
            var showInfo = temp.Split('^');
            var channel = showInfo[0];
            var show = new Show
            {
                Name = showInfo[1],
                StartTime = time.ToString("t")
            };

            // Initialize
            if (!guide.Channels.ContainsKey(channel))
            {
                guide.Channels.Add(channel, new Channel(channel));
            }

            guide.Channels[channel].Shows.Add(show);
        }

    }

}
using System;
using System.Net;
using MobileTVLibrary.Models;
using MobileTVLibrary.Helpers;

namespace MobileTVLibrary.Services.TvGuides
{
    /// <summary>Retrieve Tv Guide information from Tv Rage</summary>
    public class TvRage : ITvGuideService
    {
        /// <summary>Retrieve the Schedule form Tv Rage</summary>
        /// <param name="shouldCategorizeByChannel">True if we should categorize by channel; otherwise, categorize by timeslot</param>
        /// <returns>Populated Guide</returns>
        public Guide Retrieve(bool shouldCategorizeByChannel)
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
                        ParseShow(guide, time, line, shouldCategorizeByChannel);
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

        private static void ParseShow(Guide guide, DateTimeOffset time, string line, bool shouldCategorizeByChannel)
        {
            var temp = line.Substring(6, line.Length - 13);
            var showInfo = temp.Split('^');
            var channel = showInfo[0];
            var timeSlot = time.ToString("t");
            var show = new Show
            {
                ChannelName = channel,
                Name = showInfo[1],
                StartTime = timeSlot,
                Description = "Episode " + showInfo[2]
            };

            var categoryName = shouldCategorizeByChannel ? channel : timeSlot;

            // Initialize
            if (!guide.Category.ContainsKey(categoryName))
            {
                guide.Category.Add(categoryName, new Category(categoryName));
            }

            guide.Category[categoryName].Shows.Add(show);
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using MobileTVLibrary.Helpers;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    /// <summary>
    /// Parses Fx Network's Schedule
    /// </summary>
    public class FxNetworksParser : IChannelParser
    {
        /// <summary>
        /// Retrieve the Show list based on the provided service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            var doc = service.Get();

            var shows = from showRow in doc.DocumentNode.QuerySelectorAll(DetermineShowRowSelector(service.Date))
                        select new Show
                                   {
                                       ChannelName = "FX",
                                       Description = "",
                                       Name = showRow.QuerySelector("strong").InnerText,
                                       StartTime = showRow.QuerySelector("p:first-child").InnerText
                                   };

            // TODO: name won't work right with movies
            // TODO: parse out the date on the start time
            // TODO: Determine show length, will need to do it off the width of the li's (I know it's weird)

            return shows;
        }

        private string DetermineShowRowSelector(DateTime requestedDate)
        {
            // TODO: Make more checks so we can't go tooo far out into the future

            // Each day gets it's own day_divs with today being the first row
            var dateDifference = requestedDate - EasternTimeZone.Today;
            var daysDifference = (int) dateDifference.TotalDays;

            if (daysDifference < 0)
            {
                throw new InvalidOperationException("Can't request a date prior to today");
            }

            return string.Format(".day_divs:nth-child({0}) li", daysDifference + 1);
        }
    }
}

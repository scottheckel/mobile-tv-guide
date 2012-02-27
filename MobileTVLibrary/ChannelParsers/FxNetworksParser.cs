using System;
using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
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
        /// <param name="service">Fx Network Channel Service</param>
        /// <returns>A list of shows</returns>
        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            var doc = service.Get();

            var shows = from showRow in doc.DocumentNode.QuerySelectorAll(DetermineShowRowSelector(service.Date))
                        select new Show
                                   {
                                       ChannelName = "FX",
                                       Description = "",
                                       Name = DetermineShowName(showRow),
                                       StartTime = showRow.QuerySelector("p:first-child").InnerText,
                                       Length = DetermineLengthFromStyleWidth(showRow.Attributes["style"].Value)
                                   };

            // TODO: parse out the date on the start time
            // TODO: Determine show length, will need to do it off the width of the li's (I know it's weird)
            // TODO: Description would be a network call to the show description page at a url like this http://www.fxnetworks.com/get_show_detail.php?show_code=429646&show_time=Tue%20Feb%2028th%2011:00pm

            return shows;
        }

        /// <summary>
        /// Determine the time length of the show
        /// </summary>
        /// <param name="styleAttributeValue">Style attribute's content value</param>
        /// <returns>The running length</returns>
        private int DetermineLengthFromStyleWidth(string styleAttributeValue)
        {
            // HACK: Using display values to get a show length... yummy
            int pixelWidthStart = styleAttributeValue.IndexOf("width:", StringComparison.CurrentCulture) + 6;
            string pixelWidthText = styleAttributeValue.Substring(pixelWidthStart, styleAttributeValue.IndexOf("px", pixelWidthStart, StringComparison.CurrentCulture) - pixelWidthStart);
            
            int pixelWidth;
            if (int.TryParse(pixelWidthText, out pixelWidth))
            {
                // 30 minutes is 176 pixels right now
                return (pixelWidth/176)*30;
            }

            return 30;
        }

        /// <summary>
        /// Determine the show name, this is needed to parse out the FX Movie and get the actual name 
        /// </summary>
        /// <param name="showRow">Show Row's HTML Node</param>
        /// <returns>Show/Movie Name</returns>
        private string DetermineShowName(HtmlNode showRow)
        {
            var showName = showRow.QuerySelector("strong").InnerText;
            if (showName == "FX Movie")
            {
                // HACK: Yeah, this is a bit of a hack
                showName = showRow.QuerySelector("span p").InnerHtml;
                showName = showName.Substring(showName.IndexOf("</strong>&nbsp;", StringComparison.CurrentCulture) + 15);
            }
            return showName;
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
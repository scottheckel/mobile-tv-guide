using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fizzler.Systems.HtmlAgilityPack;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    public class FxNetworksParser : IChannelParser
    {

        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            var doc = service.Get();

            var shows = from showRow in doc.DocumentNode.QuerySelectorAll(".day_divs:first li")
                        select new Show
                                   {
                                       ChannelName = "FX",
                                       Description = "",
                                       Name = showRow.QuerySelector("strong").InnerText,
                                       StartTime = showRow.QuerySelector("p:first").InnerText
                                   };

            // TODO: name won't work right with movies
            // TODO: parse out the date on the start time
            // TODO: Determine show length, will need to do it off the width of the li's (I know it's weird)
            // TODO: do other dates in here as current date is :first

            return shows;
        }
    }
}

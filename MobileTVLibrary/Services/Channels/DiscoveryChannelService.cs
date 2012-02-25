using System;
using HtmlAgilityPack;

namespace MobileTVLibrary.Services.Channels
{
    public class DiscoveryChannelService : IChannelService
    {
        /// <summary>Date to retrieve channel data for</summary>
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date">Date to Retrieve Channel Data for</param>
        public DiscoveryChannelService(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// Retrieves the Document
        /// </summary>
        /// <returns></returns>
        public HtmlDocument Get()
        {
            // TODO: Use day variable, currently always returns today
            HtmlWeb web = new HtmlWeb();
            return web.Load("http://dsc.discovery.com/tv-schedules/daily.html");
        }
    }
}

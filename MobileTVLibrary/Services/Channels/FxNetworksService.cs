using System;
using HtmlAgilityPack;

namespace MobileTVLibrary.Services.Channels
{
    public class FxNetworksService : IChannelService
    {
        /// <summary>Date to retrieve channel data for</summary>
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date">Date to Retrieve Channel Data for</param>
        public FxNetworksService(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// Retrieves the Document
        /// </summary>
        /// <returns>Html Document</returns>
        public HtmlDocument Get()
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load("http://www.fxnetworks.com/schedule.php");
        }
    }
}

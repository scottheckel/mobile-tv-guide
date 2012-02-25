using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace MobileTVLibrary.Services.Channels
{
    public interface IChannelService
    {
        /// <summary>
        /// Date to retrieve channel data for
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Retrieves the Document Node of the Guide
        /// </summary>
        /// <param name="day">Day to display</param>
        /// <returns></returns>
        HtmlDocument Get();
    }
}

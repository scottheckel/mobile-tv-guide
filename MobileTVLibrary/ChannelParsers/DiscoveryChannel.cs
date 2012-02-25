using System;
using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    public class DiscoveryChannel : IChannelParser
    {
        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            var doc = service.Get();

            var shows = from showRow in doc.DocumentNode.QuerySelectorAll("div.contentFrame tr")
                        select new Show
                        {
                            ChannelName = "Discovery",
                            Description = "",
                            Name = showRow.QuerySelector("td:nth-child(3) strong").InnerText,
                            StartTime = showRow.QuerySelector("td:nth-child(1) div.cellPad").InnerText
                        };

            return CleanName(CleanTimeData(shows));
        }

        /// <summary>
        /// Retrieves the Document Node of the Guide
        /// </summary>
        /// <param name="day">Day to display</param>
        /// <returns></returns>
        private HtmlNode RetrieveGuideDocument(DateTime day)
        {
            // TODO: Use day variable, currently always returns today
            HtmlDocument doc;

            if (string.IsNullOrEmpty(dailyHtml))
            {
                var web = new HtmlWeb();
                doc = web.Load("http://dsc.discovery.com/tv-schedules/daily.html");
            }
            else
            {
                doc = new HtmlDocument();
                doc.LoadHtml(dailyHtml);
            }

            return doc.DocumentNode;
        }

        /// <summary>
        /// Cleans up the text in the Name of the show
        /// </summary>
        /// <param name="shows">Shows to process</param>
        /// <returns>Shows as processed</returns>
        private IEnumerable<Show> CleanName(IEnumerable<Show> shows)
        {
            foreach (var show in shows)
            {
                show.Name = show.Name.Replace("\r", "").Replace("\n", "");
                yield return show;
            }
        }

        /// <summary>
        /// Populates the length information and removes it from the start time string
        /// </summary>
        /// <param name="shows">Shows to process</param>
        /// <returns>Shows as processed</returns>
        private IEnumerable<Show> CleanTimeData(IEnumerable<Show> shows)
        {
            foreach (var show in shows)
            {
                // Grab out the length
                var lengthText = show.StartTime.Substring(show.StartTime.IndexOf('(') + 1, 2);
                int length;
                if (int.TryParse(lengthText, out length))
                {
                    show.Length = length;
                }

                // Clean the start time
                var startTime = show.StartTime.Replace("&#xA0;", " ");
                show.StartTime = startTime.Substring(0, startTime.IndexOf('m') + 1);
                yield return show;
            }
        }
    }
}

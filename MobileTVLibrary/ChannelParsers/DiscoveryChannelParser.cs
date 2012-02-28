using System;
using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    /// <summary>
    /// Parses Discovery Channel's Schedule and other related networks like Animal Planet
    /// </summary>
    public class DiscoveryChannelParser : IChannelParser
    {
        /// <summary>
        /// Retrieve the Show list based on the provided service
        /// </summary>
        /// <param name="service">Discovery Network Channel Service</param>
        /// <returns>A list of shows</returns>
        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            var doc = service.Get();

            var shows = from showRow in doc.DocumentNode.QuerySelectorAll("div.contentFrame tr")
                        select new Show
                        {
                            ChannelName = "Discovery",
                            Description = BuildDescription(showRow),
                            EpisodeTitle = showRow.QuerySelector("td:nth-child(3) em").InnerText,
                            Name = showRow.QuerySelector("td:nth-child(3) strong").InnerText,
                            StartTime = showRow.QuerySelector("td:nth-child(1) div.cellPad").InnerText
                        };

            return CleanName(CleanTimeData(shows));
        }

        /// <summary>Build's a description from a show row</summary>
        /// <param name="showRow">Show Row Node</param>
        /// <returns>Description Text</returns>
        private string BuildDescription(HtmlNode showRow)
        {
            // HACK: They don't stick the description in it's own element, so you need to grab it out from inside of the table cell
            var description = showRow.QuerySelector("td:nth-child(3) div").InnerHtml;
            description = description.Substring(description.IndexOf("<br class=\"lineHeight5\">", StringComparison.CurrentCulture) + 24);
            description = description.Substring(0, description.Length - 6); // remove the final <br> tag
            return description;
        }

        /// <summary>
        /// Cleans up the text in the Name of the show
        /// </summary>
        /// <param name="shows">Shows to process</param>
        /// <returns>Shows as processed</returns>
        private IEnumerable<Show> CleanName(IEnumerable<Show> shows)
        {
            // TODO: You can run into these weird things where they have subtitles for the shows like "World's Toughest Trucker - (World's Toughest Trucker)", which we only really want it to say "World's Toughest Trucker"

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
            // TODO: What are we going to do with the date portion? We're not putting it in right now

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
                var startTime = show.StartTime.Replace("&#xA0;", " ").Replace(" ", ""); // removing a character entity and the space between the time and the am/pm
                show.StartTime = startTime.Substring(0, startTime.IndexOf('m') + 1);
                yield return show;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTVGuide.Helpers
{
    /// <summary>Helps work with times that are assumed to be in the easter time zone</summary>
    public static class EasternTimeZone
    {
        private static TimeZoneInfo timeZoneInfo;

        /// <summary>Current Eastern Date/Time</summary>
        public static DateTimeOffset Now
        {
            get
            {
                if (timeZoneInfo == null)
                {
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                }
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            }
        }

        /// <summary>Current Eastern Date</summary>
        public static DateTime Today
        {
            get
            {
                return Now.Date;
            }
        }

        /// <summary>Try to parse the date/time string as an eastern timezone date if it can't be parsed take the default</summary>
        /// <param name="dateTimeString">Date/time string to parse</param>
        /// <param name="output">DateTimeOffset for output</param>
        /// <returns>True if parsing was ok</returns>
        public static DateTimeOffset Parse(string dateTimeString, DateTimeOffset defaultDateTime)
        {
            // TODO: need to convert this to eastern
            DateTimeOffset output;
            if (!DateTimeOffset.TryParse(dateTimeString, out output))
            {
                output = defaultDateTime;
            }
            return output;
        }
    }
}
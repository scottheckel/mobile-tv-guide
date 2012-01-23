using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTVGuide.Models
{
    public class Show
    {
        /// <summary>
        /// Channel Name
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// Show Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Start Time
        /// </summary>
        public string StartTime { get; set; }
    }
}
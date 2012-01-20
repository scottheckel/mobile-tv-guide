using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTVGuide.Models
{
    public class Channel
    {
        /// <summary>
        /// Channel Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Shows currently on this channel
        /// </summary>
        public IList<Show> Shows { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Channel Name</param>
        public Channel(string name)
        {
            Name = name;
            Shows = new List<Show>();
        }
    }
}
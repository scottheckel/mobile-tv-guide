using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTVGuide.Models
{
    /// <summary>
    /// Represents all Guide data
    /// </summary>
    public class Guide
    {
        /// <summary>
        /// Channels listed in the Guide
        /// </summary>
        public IDictionary<string, Channel> Channels
        {
            get;
            internal set;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Guide()
        {
            Channels = new Dictionary<string, Channel>();
        }
    }
}
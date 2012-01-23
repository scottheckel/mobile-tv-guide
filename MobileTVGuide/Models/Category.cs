using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTVGuide.Models
{
    public class Category
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
        /// <param name="name">Category Name</param>
        public Category(string name)
        {
            Name = name;
            Shows = new List<Show>();
        }
    }
}
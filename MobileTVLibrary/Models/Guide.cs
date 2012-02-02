using System.Collections.Generic;

namespace MobileTVLibrary.Models
{
    /// <summary>
    /// Represents all Guide data
    /// </summary>
    public class Guide
    {
        /// <summary>
        /// Category the Guide is broken into current
        /// </summary>
        public IDictionary<string, Category> Category
        {
            get;
            internal set;
        }

        /// <summary>
        /// Pagination next
        /// </summary>
        public string NextUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Pagination previous
        /// </summary>
        public string PreviousUrl
        {
            get; set;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Guide()
        {
            Category = new Dictionary<string, Category>();
        }
    }
}
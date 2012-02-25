using MobileTVLibrary.ChannelParsers;
using MobileTVLibrary.Services.TvGuides;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MobileTVLibrary.Models;
using System.Collections.Generic;

namespace MobileTVLibraryTest
{
    
    
    /// <summary>
    ///This is a test class for DiscoveryChannelTest and is intended
    ///to contain all DiscoveryChannelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DiscoveryChannelTest
    {

        /// <summary>
        ///A test for RetrieveShows
        ///</summary>
        [TestMethod()]
        public void RetrieveShowsTest()
        {
            var service = new DiscoveryChannel(Resources.Discovery);
            var day = new DateTime();
            var actual = service.RetrieveShows(day);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}

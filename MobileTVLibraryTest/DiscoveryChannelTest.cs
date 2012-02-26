using MobileTVLibrary.ChannelParsers;
using MobileTVLibrary.Services.TvGuides;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MobileTVLibrary.Models;
using System.Collections.Generic;
using MobileTVLibrary.Services.Channels;

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
            var parser = new DiscoveryChannel();
            var day = new DateTime();
            var actual = parser.RetrieveShows(new DiscoveryChannelService(day));
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}

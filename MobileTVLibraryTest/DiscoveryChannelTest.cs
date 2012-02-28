using System.Linq;
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
            var result = parser.RetrieveShows(new FakeChannelService(Resources.Discovery));
            Assert.IsNotNull(result);
            Assert.AreEqual(34, result.Count());

            var aShow = result.First();
            Assert.AreEqual("Discovery", aShow.ChannelName);
            Assert.AreEqual("Australian Outback: The First Elimination", aShow.EpisodeTitle);
            Assert.AreEqual("After hauling oversized loads across the remote and grueling unpaved roads of the Australian Outback, the truckers face elimination where the two weakest are sent packing. Only the toughest truckers will survive.", aShow.Description);
            Assert.AreEqual(60, aShow.Length);
            Assert.AreEqual("World's Toughest Trucker - (World's Toughest Trucker)", aShow.Name);
            Assert.AreEqual("12:00am", aShow.StartTime);
            
        }
    }
}

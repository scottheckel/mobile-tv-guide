using System.Linq;
using HtmlAgilityPack;
using MobileTVLibrary.ChannelParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileTVLibrary.Helpers;

namespace MobileTVLibraryTest
{
    /// <summary>
    ///This is a test class for FxNetworksParserTest and is intended
    ///to contain all FxNetworksParserTest Unit Tests
    ///</summary>
    [TestClass]
    public class FxNetworksParserTest
    {
        ///// <summary>
        /////A test for DetermineShowRowSelector
        /////</summary>
        //[TestMethod]
        //public void DetermineShowRowSelectorTest()
        //{
        //    FxNetworksParser parser = new FxNetworksParser();
        //    string selector = parser.DetermineShowRowSelector(EasternTimeZone.Today);
        //    Assert.AreEqual(".day_divs:nth-child(1) li", selector);

        //    selector = parser.DetermineShowRowSelector(EasternTimeZone.Today.AddDays(2));
        //    Assert.AreEqual(".day_divs:nth-child(3) li", selector);
        //}

        /// <summary>
        ///A test for RetrieveShows
        ///</summary>
        [TestMethod]
        public void RetrieveShowsTest()
        {
            FxNetworksParser parser = new FxNetworksParser();
            var result = parser.RetrieveShows(new FakeChannelService(Resources.FXSchedule) { Date = EasternTimeZone.Today.AddDays(1) });
            Assert.IsNotNull(result);
            Assert.AreEqual(27, result.Count());

            var aShow = result.ToList()[18];
            Assert.AreEqual("FX", aShow.ChannelName);
            Assert.AreEqual("", aShow.Description);
            Assert.AreEqual(30, aShow.Length);
            Assert.AreEqual("How I Met Your Mother", aShow.Name);
            Assert.AreEqual("Tue Feb 28th 4:30pm", aShow.StartTime);
        }

        //[TestMethod]
        //public void DetermineLengthFromStyleWidthTest()
        //{
        //    FxNetworksParser parser = new FxNetworksParser();
        //    var length = parser.DetermineLengthFromStyleWidth("width:176px");
        //    Assert.AreEqual(30, length);
        //    length = parser.DetermineLengthFromStyleWidth("width:1056px");
        //    Assert.AreEqual(180, length);
        //    length = parser.DetermineLengthFromStyleWidth("width:880px");
        //    Assert.AreEqual(150, length);
        //}

        //[TestMethod]
        //public void DetermineShowNameTest()
        //{
        //    FxNetworksParser parser = new FxNetworksParser();
        //    var name = parser.DetermineShowName(new HtmlNode(HtmlNodeType.Element, new HtmlDocument(), 0) { InnerHtml = "<span><p><strong>Paid Programming</strong></p></span>" });
        //    Assert.AreEqual("Paid Programming", name);
        //    name = parser.DetermineShowName(new HtmlNode(HtmlNodeType.Element, new HtmlDocument(), 0) { InnerHtml = "<span><p><strong>FX Movie</strong>&nbsp;The Happening</p></span>" });
        //    Assert.AreEqual("The Happening", name);
        //}
    }
}

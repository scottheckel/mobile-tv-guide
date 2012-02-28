using HtmlAgilityPack;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibraryTest
{
    public class FakeChannelService : IChannelService
    {
        private string html;
        public FakeChannelService(string htmlToReturn)
        {
            html = htmlToReturn;
        }

        public System.DateTime Date { get; set; }

        public HtmlDocument Get()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
    }
}

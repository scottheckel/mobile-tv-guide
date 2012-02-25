using System.Collections.Generic;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    public interface IChannelParser
    {
        IEnumerable<Show> RetrieveShows(IChannelService service);
    }
}

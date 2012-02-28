using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileTVLibrary.Models;
using MobileTVLibrary.Services.Channels;

namespace MobileTVLibrary.ChannelParsers
{
    public class TNTParser : IChannelParser
    {
        #region IChannelParser Members

        public IEnumerable<Show> RetrieveShows(IChannelService service)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

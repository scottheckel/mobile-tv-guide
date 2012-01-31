using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileTVGuide.Models;

namespace MobileTVGuide.Services.TvGuides
{
    public interface ITvGuideService
    {
        Guide Retrieve(bool shouldCategorizeByChannel);
    }
}

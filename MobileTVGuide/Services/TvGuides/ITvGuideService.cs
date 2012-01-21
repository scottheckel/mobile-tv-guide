using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileTVGuide.Models;

namespace MobileTVGuide.Services.TvGuides
{
    interface ITvGuideService
    {
        Guide Retrieve();
    }
}

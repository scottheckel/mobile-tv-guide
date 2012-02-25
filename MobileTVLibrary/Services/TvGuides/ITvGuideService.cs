using MobileTVLibrary.Models;

namespace MobileTVLibrary.Services.TvGuides
{
    public interface ITvGuideService
    {
        Guide Retrieve(bool shouldCategorizeByChannel);
    }
}

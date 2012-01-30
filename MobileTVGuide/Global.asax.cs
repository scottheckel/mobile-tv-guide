using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;
using MobileTVGuide.Models;
using MobileTVGuide.Services.TvGuides;

namespace MobileTVGuide
{

    public class MvcApplication : System.Web.HttpApplication
    {
        private static CacheItemRemovedCallback OnCacheRemoveForCacheTask = null;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GuideChannel",
                "channel",
                new { controller = "Guide", action = "Channel" }
            );

            routes.MapRoute(
                "GuideTimeslot",
                "timeslot/{startHour}",
                new {controller = "Guide", action = "Timeslot", startHour = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AddCacheTask("UpdateGuide", 60);
        }

        private void AddCacheTask(string key, int seconds)
        {
            OnCacheRemoveForCacheTask = new CacheItemRemovedCallback(CacheItemRemovedForCacheTask);
            HttpRuntime.Cache.Insert(key, seconds, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemoveForCacheTask);
        }

        /// <summary>
        /// Execute the task. Called when the cache item is removed.
        /// </summary>
        /// <param name="key">Key/Name</param>
        /// <param name="value">Cycle that this task previously ran at</param>
        /// <param name="reason">Reason for cache item being removed</param>
        public void CacheItemRemovedForCacheTask(string key, object value, CacheItemRemovedReason reason)
        {
            int secondsToRunAgainAt = Convert.ToInt32(value);
            if (string.Equals(key, "UpdateGuide", StringComparison.Ordinal))
            {
                ITvGuideService tvGuide = new TvRage();
                Guide guide = tvGuide.Retrieve(true); // SH - this obviously does nothing right now, the next task will make this better

                secondsToRunAgainAt = 3600; // run every hour right now
            }

            // re-add our task so it recurs
            AddCacheTask(key, secondsToRunAgainAt);
        }
    }
}
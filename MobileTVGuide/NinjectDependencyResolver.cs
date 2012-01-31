using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Ninject.Syntax;

namespace MobileTVGuide
{
    /// <summary>
    /// Ninject Dependency Resolver
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot resolutionRoot;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kernel">Kernel</param>
        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            resolutionRoot = kernel;
        }

        /// <summary>
        /// Get a service
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <returns>Service</returns>
        public object GetService(Type serviceType)
        {
            return resolutionRoot.TryGet(serviceType);
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="serviceType">Service Type</param>
        /// <returns>Services of type</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return resolutionRoot.GetAll(serviceType);
        }

    }

}
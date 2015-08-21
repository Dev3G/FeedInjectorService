using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedInjector.Infrastructure.Extensions
{
    public static class PipelineServiceProviderExtensions
    {
        internal static List<ServiceParameterAttribute> GetServiceParameters(this IPipelineServiceProvider sp, ServiceParameterType serviceParameterType)
        {
            List<ServiceParameterAttribute> paramList = new List<ServiceParameterAttribute>();
            foreach (var attribute in sp.GetType().GetCustomAttributes(typeof(ServiceParameterAttribute), true))
                paramList.Add(attribute as ServiceParameterAttribute);

            return paramList.Where(p => p.Type == serviceParameterType).ToList();
        }
    }
}
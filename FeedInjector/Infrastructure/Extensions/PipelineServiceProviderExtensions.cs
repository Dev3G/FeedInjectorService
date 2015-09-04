using FeedInjector.Common.Models;
using FeedInjector.Common.Services;
using FeedInjector.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedInjector.Infrastructure.Extensions
{
    public static class PipelineServiceProviderExtensions
    {
        /// <summary>
        /// Returns a <list type="ServiceParameterAttribute">list</list> of attrbutes that match the query
        /// </summary>
        /// <param name="sp"><typeparamref name="IPipelineServiceProvider">Pipeline Serivce Provider</typeparamref> to query</param>
        /// <param name="serviceParameterType">The type of <typeparamref name="ServiceParameterAttribute">ServiceParameterAttributes</typeparamref> to query</param>
        /// <returns></returns>
        internal static List<ServiceParameterAttribute> GetServiceParameters(this IPipelineServiceProvider sp, ServiceParameterType serviceParameterType)
        {
            List<ServiceParameterAttribute> paramList = new List<ServiceParameterAttribute>();
            foreach (var attribute in sp.GetType().GetCustomAttributes(typeof(ServiceParameterAttribute), true))
                paramList.Add(attribute as ServiceParameterAttribute);

            return paramList.Where(p => p.Type == serviceParameterType).ToList();
        }
    }
}
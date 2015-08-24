using FeedInjector.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;

namespace FeedInjector.Infrastructure
{
    internal class ServiceProviderCompositionContainer
    {
        internal CompositionContainer Container { get; set; }
        internal ServiceProviderCompositionContainer()
        {
            //TODO: Folder in web.config
            var catalog = new DirectoryCatalog("bin/providers");
            Container = new CompositionContainer(catalog);
            Container.ComposeParts();
        }

        /// <summary>
        /// Returns a service provider
        /// </summary>
        /// <param name="contractName"></param>
        /// <returns></returns>
        internal IPipelineServiceProvider GetProvider(string contractName)
        {
            try
            {
                var provider = Container.GetExport<IPipelineServiceProvider>(contractName).Value;
                provider.Parameters = new Dictionary<string, string>();
                return provider;
            }
            catch (Exception ex)
            {
                throw new CompositionContractMismatchException(string.Format("'{0}' not found. Available components are exposed through the /Help url", ex));
            }
        }

        internal IEnumerable<IPipelineServiceProvider> GetAllProviders()
        {
            return Container.GetExportedValues<IPipelineServiceProvider>();
        }
    }
}
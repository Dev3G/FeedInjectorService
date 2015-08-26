using FeedInjector.Common.Infrastructure;
using FeedInjector.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Provider.DataFactory.Services
{
    [Export(RssParser.ContractName, typeof(IPipelineServiceProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    [ServiceParameter(ServiceParameterType.Input, "rssFeedUri", true, "The uri of the rss feed")]
    [ServiceParameter(ServiceParameterType.Input, "rssUser", true, "The uri of the rss feed")]
    [ServiceParameter(ServiceParameterType.Input, "rssPassword", true, "The uri of the rss feed")]
    [ServiceParameter(ServiceParameterType.Output, "news.title", true, "The title of the news object")]

    public class RssParser : IPipelineServiceProvider
    {
        public const string ContractName = "ParseRss";

        public string Name { get { return ContractName; } }

        public string Description
        {
            get { return "Parses the specified rss feed to gather news"; }
        }

        public void ProcessPipeline(Common.Models.WorkflowModel model)
        {
        }
    }
}

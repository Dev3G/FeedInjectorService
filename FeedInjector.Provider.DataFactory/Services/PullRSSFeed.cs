using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FeedInjector.Provider.DataFactory
{
    [Export(typeof(IPipelineServiceProvider))]
    [Export(PullRssFeedService.ContractName, typeof(IPipelineServiceProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    [ServiceParameter(ServiceParameterType.Input, "imgPath", true, "Ruta donde se guarda imagen")]
    [ServiceParameter(ServiceParameterType.Input, "imgId", true, "Id de la imagen")]
    [ServiceParameter(ServiceParameterType.Output, "imgPath", true, "Ruta donde se guarda imagen")]
    [ServiceParameter(ServiceParameterType.Output, "creationId", false, "Id de la imagen")]
    public class PullRssFeedService : IPipelineServiceProvider
    {
        public const string ContractName = "PullRssFeed";

        public string Name { get { return ContractName; } }

        public Dictionary<string, string> Parameters { get; set; }

        public void ProcessPipeline(Common.Models.WorkflowModel model)
        {
            Console.WriteLine(model.Changelog.Count);
        }   

        public string Description
        {
            get
            {
                return "Pull rss feed from Data Factory";
            }
        }
    }
}

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


        public List<ServiceParameterModel> ContractInputs
        {
            get
            {
                return new List<ServiceParameterModel>()
                {
                    new ServiceParameterModel("imgPath", true, "Ruta donde se guarda imagen"),
                    new ServiceParameterModel("imgId", true, "Id de la imagen")
                };
            }
        }

        public List<ServiceParameterModel> ContractOutputs
        {
            get
            {
                return new List<ServiceParameterModel>()
                {
                    new ServiceParameterModel("imgPath", true, "Ruta donde se guarda imagen"),
                    new ServiceParameterModel("creationId",false, "Id de la imagen")
                };
            }
        }
    }
}

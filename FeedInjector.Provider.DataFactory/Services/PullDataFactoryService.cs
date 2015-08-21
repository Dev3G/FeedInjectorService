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
    [Export(PullDataFactoryService.ContractName, typeof(IPipelineServiceProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    [ServiceParameter(ServiceParameterType.Input, "ftpUser", true, "Ftp username")]
    [ServiceParameter(ServiceParameterType.Input, "ftpPassword", false, "Ftp password")]
    [ServiceParameter(ServiceParameterType.Output, "imgPath", true, "Ruta donde se guarda imagen")]
    [ServiceParameter(ServiceParameterType.Output, "imgId", true, "Id de la imagen")]
    public class PullDataFactoryService : IPipelineServiceProvider
    {
        public const string ContractName = "PullDataFactoryService";
        public string Name { get { return ContractName; } }

        public Dictionary<string, string> Parameters {get;set;}

        public void ProcessPipeline(Common.Models.WorkflowModel model)
        {
            Console.WriteLine(model.Changelog.Count);
        }

        public string Description
        {
            get
            {
                return "Pull FTP soccer data from DataFactory";
            }
        }
    }
}

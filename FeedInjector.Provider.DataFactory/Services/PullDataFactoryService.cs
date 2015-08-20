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


        public List<ServiceParameterModel> ContractInputs
        {
            get
            {
                return new List<ServiceParameterModel>()
                {
                    new ServiceParameterModel("ftpUser", true, "Id de cuerpo de RealInfo"),
                    new ServiceParameterModel("ftpPassword",false, "Id de noticia de RealInfo")
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
                    new ServiceParameterModel("imgId",true, "Id de la imagen")
                };
            }
        }
    }
}

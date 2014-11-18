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
        public string ServiceName { get { return ContractName; } }

        public Dictionary<string, string> Parameters {get;set;}

        public void ProcessPipeline(Common.Models.PipelineModel model)
        {
            Console.WriteLine(model.Changelog.Count);
        }

        public string ServiceDescription
        {
            get
            {
                return "Pull FTP soccer data from DataFactory";
            }
        }


        public List<PipelineParameterModel> ContractInputs
        {
            get
            {
                return new List<PipelineParameterModel>()
                {
                    new PipelineParameterModel("ftpUser", true, "Id de cuerpo de RealInfo"),
                    new PipelineParameterModel("ftpPassword",false, "Id de noticia de RealInfo")
                };
            }
        }

        public List<PipelineParameterModel> ContractOutputs
        {
            get
            {
                return new List<PipelineParameterModel>()
                {
                    new PipelineParameterModel("imgPath", true, "Ruta donde se guarda imagen"),
                    new PipelineParameterModel("imgId",true, "Id de la imagen")
                };
            }
        }
    }
}

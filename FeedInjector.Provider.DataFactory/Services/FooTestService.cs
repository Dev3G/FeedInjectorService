using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Provider.DataFactory.Services
{
    [Export(typeof(IPipelineServiceProvider))]
    [Export(FooTestService.ContractName, typeof(IPipelineServiceProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FooTestService : IPipelineServiceProvider
    {
        public const string ContractName = "FooTestService";

        public string ServiceName { get { return ContractName; } }

        public Dictionary<string, string> Parameters { get; set; }

        public void ProcessPipeline(Common.Models.PipelineModel model)
        {
            Console.WriteLine(model.Changelog.Count);
        }

        public void Log(Common.Models.PipelineModel model)
        {
            model.Changelog.Add(new Common.Models.ChangelogModel(this.GetType().ToString(), ServiceDescription));
        }

        public string ServiceDescription
        {
            get
            {
                return "test con yonatan";
            }
        }


        public List<PipelineParameterModel> ContractInputs
        {
            get
            {
                return new List<PipelineParameterModel>()
                {
                    new PipelineParameterModel("cuerpoId", true, "Id de cuerpo de RealInfo"),
                    new PipelineParameterModel("noticiaId",false, "Id de noticia de RealInfo")
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
                    new PipelineParameterModel("creationId",false, "Id de la imagen")
                };
            }
        }
    }
}

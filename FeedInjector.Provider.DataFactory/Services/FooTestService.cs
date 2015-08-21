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

    [ServiceParameter(ServiceParameterType.Input, "cuerpoId", true, "Id de cuerpo de RealInfo")]
    [ServiceParameter(ServiceParameterType.Input, "noticiaId", false, "Id de noticia de RealInfo")]
    [ServiceParameter(ServiceParameterType.Output, "imgPath", true, "Ruta donde se guarda imagen")]
    [ServiceParameter(ServiceParameterType.Output, "creationId", false, "Id de la imagen")]
    public class FooTestService : IPipelineServiceProvider
    {
        public const string ContractName = "FooTestService";

        public string Name { get { return ContractName; } }

        public Dictionary<string, string> Parameters { get; set; }

        public void ProcessPipeline(Common.Models.WorkflowModel model)
        {
            Console.WriteLine(model.Changelog.Count);
        }

        public void Log(Common.Models.WorkflowModel model)
        {
            model.Changelog.Add(new Common.Models.ChangelogModel(this.GetType().ToString(), Description));
        }

        public string Description
        {
            get
            {
                return "test";
            }
        }
    }
}

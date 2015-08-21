using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
using FeedInjector.Filters;
using FeedInjector.Infrastructure;
using FeedInjector.Models;
using FeedInjector.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FeedInjector.Controllers
{
    [RoutePrefix("api/Pipeline")]

    public class PipelineController : ApiController
    {
        //[Route("Evento")]
        //public EventoModel Post([FromBody]EventoModel evento)
        //{
        //    return evento;
        //}

        //[Route("Resultado")]
        //public ResultadoModel Post([FromBody]ResultadoModel resultado)
        //{
        //    return resultado;
        //}

        [Route("PostPlugin")]
        public string Post([FromBody]PipelineWorkflowModel plugin)
        {
            var container = new ServiceProviderCompositionContainer();

            var pipeline = container.GetProvider(plugin.Name);

            return pipeline.Description;
        }

        //?workflow=PullRssFeed(par1:val1,par2:val2)@PullDataFactoryService(par3:val3,par4:val4)
        [Route("PipePlugin")]
        public PipelineWorkflow Get([FromUri]string workflow)
        {
            var providerWorkflow = PipelineWorkflowFactory.CreateWorkflow(workflow);

            providerWorkflow.Execute();

            return providerWorkflow;
        }
    }
}

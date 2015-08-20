using FeedInjector.Common.ServiceInterfaces;
using FeedInjector.Filters;
using FeedInjector.Infrastructure;
using FeedInjector.Models;
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

        [Route("Help")]
        public HttpResponseMessage Get()
        {
            var container = new ServiceProviderCompositionContainer();
            var providers = container.GetAllProviders();

            var response = new StringBuilder();

            response.AppendLine("<h1>ParameterSyntax</h1>");
            response.AppendLine("<p>Example call: <b>/?workflow=PullDataFactoryService(cuerpoId:val1,noticiaId:val2)@PullRssFeed</b></p>");
            response.AppendLine("<p>Escape characters: <b>{comma}</b>: ','</p>");
            response.AppendLine("<p>Escape characters: <b>{at}</b>: '@'</p>");

            response.AppendLine("<h1>Available pipeline controllers</h1>");

            foreach(var p in providers)
            {

                response.AppendLine(string.Format("<h2>{0}: {1}</h2>", p.Name, p.Description));

                response.AppendLine(string.Format("<h3>[Required inputs]</h3>", p.Name, p.Description));
                foreach(var param in p.ContractInputs.Where(i=>i.IsRequired).ToList())
                    response.AppendLine(string.Format("{0}: {1}", param.Name, param.Description));

                response.AppendLine(string.Format("<h3>[Optional inputs]</h3>", p.Name, p.Description));
                foreach (var param in p.ContractInputs.Where(i => !i.IsRequired).ToList())
                    response.AppendLine(string.Format("{0}: {1}", param.Name, param.Description));

                response.AppendLine(string.Format("<h3>[Required outputs]</h3>", p.Name, p.Description));
                foreach (var param in p.ContractOutputs.Where(i => i.IsRequired).ToList())
                    response.AppendLine(string.Format("{0}: {1}", param.Name, param.Description));

                response.AppendLine(string.Format("<h3>[Optional outputs]</h3>", p.Name, p.Description));
                foreach (var param in p.ContractOutputs.Where(i => !i.IsRequired).ToList())
                    response.AppendLine(string.Format("{0}: {1}", param.Name, param.Description));
            }

            var htmlResponse = new HttpResponseMessage();
            htmlResponse.Content = new StringContent(response.Replace(Environment.NewLine, "<br />").ToString());
            htmlResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return htmlResponse;
        }
    }
}

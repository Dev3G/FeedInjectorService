using FeedInjector.Common.Models;
using FeedInjector.Common.Services;
using FeedInjector.Filters;
using FeedInjector.Infrastructure;
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
        //?workflow=PullRssFeed(par1:val1,par2:val2)@PullDataFactoryService(par3:val3,par4:val4)
        [Route("PipePlugin")]
        public HttpResponseMessage Get([FromUri]string workflow)
        {
            LogExtensions.Log.DebugCall(() => new { Workflow = workflow });

            try
            {
                var providerWorkflow = PipelineWorkflowFactory.CreateWorkflow(workflow);

                var model = providerWorkflow.Execute();

                LogExtensions.Log.InfoCall(() => new { Info = "Workflow execution completed" });

                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                LogExtensions.Log.ErrorCall(ex, () => new { Workflow = workflow });

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

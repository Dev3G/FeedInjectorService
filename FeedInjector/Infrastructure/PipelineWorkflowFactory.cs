using FeedInjector.Common.Models;
using FeedInjector.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedInjector.Infrastructure
{
    public class PipelineWorkflowFactory
    {
        /// <summary>
        /// Creates an instance of a valid service provider workflow 
        /// </summary>
        /// <param name="workflowDefinitionString">Example call: PullDataFactoryService(cuerpoId:val1,noticiaId:val2)@PullRssFeed</param>
        public static PipelineWorkflow CreateWorkflow(string workflowDefinitionString)
        {
            var container = new ServiceProviderCompositionContainer();

            var workflow = new List<IPipelineServiceProvider>();
            var serviceParameterWorkflow = new Dictionary<IPipelineServiceProvider, Dictionary<string, string>>();

            var pipes = workflowDefinitionString.Split('@');
            foreach (var p in pipes)
            {
                var openIndex = p.IndexOf('(');

                var pluginName = p.Substring(0, openIndex > 0 ? openIndex : p.Length);
                var serviceProvider = container.GetProvider(pluginName);
                var serviceParameters = new Dictionary<string, string>();

                if (openIndex > 0)
                    foreach (var kvpair in p.Remove(0, openIndex + 1).TrimEnd(')').Split(','))
                    {
                        var kvarr = kvpair.Split(':');
                        serviceParameters.Add(SanitizeParameterString(kvarr[0]), SanitizeParameterString(kvarr[1]));
                    }

                workflow.Add(serviceProvider);
                serviceParameterWorkflow.Add(serviceProvider, serviceParameters);
            }

            var pipeWorkflow = new PipelineWorkflow(workflow, serviceParameterWorkflow);

            pipeWorkflow.Validate(); //Validate if all input contracts are satisfied

            return pipeWorkflow;
        }

        /// <summary>
        /// Replaces the escaped parameters from a string
        /// </summary>
        /// <param name="parameter">Parameter to saniize</param>
        /// <returns></returns>
        private static string SanitizeParameterString(string parameter)
        {
            return parameter.Replace("{comma}", ",").Replace("{at}", "@");
        }
    }
}
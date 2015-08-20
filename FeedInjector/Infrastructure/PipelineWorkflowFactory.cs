using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
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
        /// <param name="workflow">List of service providers representing the workflow</param>
        public static PipelineWorkflow CreateWorkflow(List<IPipelineServiceProvider> workflow)
        {
            var pipeWorkflow = new PipelineWorkflow(workflow);
            pipeWorkflow.Validate();
            return pipeWorkflow;
        }

        /// <summary>
        /// Creates an instance of a valid service provider workflow 
        /// </summary>
        /// <param name="workflowDefinitionString">Example call: PullDataFactoryService(cuerpoId:val1,noticiaId:val2)@PullRssFeed</param>
        public static PipelineWorkflow CreateWorkflow(string workflowDefinitionString)
        {
            var container = new ServiceProviderCompositionContainer();

            var workflow = new List<IPipelineServiceProvider>();

            var pipes = workflowDefinitionString.Split('@');
            foreach (var p in pipes)
            {
                var openIndex = p.IndexOf('(');
                var closingIndex = p.LastIndexOf(')');

                var pluginName = p.Substring(0, openIndex > 0 ? openIndex : p.Length);
                var serviceProvider = container.GetProvider(pluginName);


                if (openIndex > 0)
                    foreach (var kvpair in p.Remove(0, openIndex + 1).TrimEnd(')').Split(','))
                    {
                        var kvarr = kvpair.Split(':');
                        serviceProvider.Parameters.Add(SanitizeParameterString(kvarr[0]), SanitizeParameterString(kvarr[1]));
                    }

                workflow.Add(serviceProvider);

            }

            var pipeWorkflow = new PipelineWorkflow(workflow);
            pipeWorkflow.Validate();
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
using FeedInjector.Common.Models;
using FeedInjector.Common.Services;
using FeedInjector.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedInjector.Infrastructure.Extensions;

namespace FeedInjector.Infrastructure
{
    public class PipelineWorkflow
    {
        /// <summary>
        /// Initializes a new pipeline workflow specifying the pipeline service providers and their parameters, if any
        /// </summary>
        /// <param name="workflow">A chain of pipeline service providers</param>
        /// <param name="serviceProviderParameters">The parameters for each service provider</param>
        public PipelineWorkflow(List<IPipelineServiceProvider> workflow, Dictionary<IPipelineServiceProvider, Dictionary<string, string>> serviceProviderParameters)
        {
            this.Workflow = workflow;
            this.ServiceProviderParameters = serviceProviderParameters;
        }

        /// <summary>
        /// A chain of pipeline service providers in the order that they will be executed
        /// </summary>
        public List<IPipelineServiceProvider> Workflow { get; private set; }

        /// <summary>
        /// Defines the parameters that will be sent to each pipeline service provider
        /// </summary>
        public Dictionary<IPipelineServiceProvider, Dictionary<string, string>> ServiceProviderParameters { get; private set; }

        /// <summary>
        /// Validates if the required workflow parameters for each SP are specified. Throws an ArgumentException when a service provider doesn't satisfy it's required input contract parameters
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when no output parameters of previous service providers or service parameters specified in the workflow satisfy the input contracts of a pipeline service provider in the pipeline chain</exception>
        internal void Validate()
        {
            var simulation = new List<string>();

            foreach (var sp in Workflow)
            {
                foreach (var kvp in ServiceProviderParameters[sp]) //Add each parameter
                    simulation.Add(kvp.Key);

                //Consider the outputs of previous executions
                foreach (var inputParameter in sp.GetServiceParameters(ServiceParameterType.Input).Where(p => p.IsRequired))
                    if (!simulation.Contains(inputParameter.Name)) //If no parameter satisfies the required input contract throw an exception
                        throw new ArgumentException(string.Format("Service provider '{0}' needs a parameter '{1}' ({2}), either as a direct parameter or as an output parameter of the previous service provider in the pipeline chain.", sp.Name, inputParameter.Name, inputParameter.Description));

                foreach (var outputParameter in sp.GetServiceParameters(ServiceParameterType.Output).Where(p => p.IsRequired))
                    simulation.Add(outputParameter.Name);
            }
        }

        /// <summary>
        /// Executes the pipeline for each service provider in the workflow
        /// </summary>
        /// <exception cref="OperationCanceledException"></exception>
        internal WorkflowModel Execute()
        {
            var model = new WorkflowModel(); //Initialize data transfer between service providers

            foreach (var sp in Workflow)
            {
                var change = new ChangelogModel(sp);
                LogExtensions.Log.DebugCall(() => new { Change = change });
                model.Changelog.Add(change); //Tag each action

                try
                {
                    //Add or replace model data based on the specified parameters for this service
                    foreach (var kvp in this.ServiceProviderParameters[sp])
                        model[kvp.Key] = kvp.Value;

                    sp.ProcessPipeline(model); //Execute the current pipeline operation

                    //Validate that the service provider respected the output contract
                    foreach (var kvp in sp.GetServiceParameters(ServiceParameterType.Output).Where(output => output.IsRequired).ToList())
                        if (!model.Values.Keys.Any(key => kvp.Name == key))
                            throw new OperationCanceledException(string.Format("Workflow was cancelled because '{0}' didn't respect it's required output contract. Key '{1}' ({2}) not found", sp, kvp.Name, kvp.Description));
                }
                catch (Exception ex)
                {
                    model.Exception = ex;
                    throw new OperationCanceledException(string.Format("Workflow was cancelled because '{0}' threw an exception", sp.Name), ex);
                }
            }

            return model;
        }

    }
}
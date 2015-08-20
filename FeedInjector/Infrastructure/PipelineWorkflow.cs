using FeedInjector.Common.Models;
using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedInjector.Infrastructure
{
    public class PipelineWorkflow
    {
        public PipelineWorkflow(List<IPipelineServiceProvider> workflow)
        {
            this.Workflow = workflow;
        }

        public List<IPipelineServiceProvider> Workflow { get; set; }

        /// <summary>
        /// Validates if the required workflow parameters for each SP are specified. Throws an ArgumentException when a service provider doesn't satisfy it's required input contract parameters
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        internal void Validate()
        {
            var simulation = new List<string>();

            foreach (var sp in Workflow)
            {
                foreach (var kvp in sp.Parameters)
                    simulation.Add(kvp.Key);

                sp.ContractInputs.Where(input => input.IsRequired).ToList().ForEach(input =>
                {
                    if (!simulation.Contains(input.Name))
                        throw new ArgumentException(string.Format("Service provider '{0}' needs a parameter '{1}' ({2}), either as a direct parameter or as an output argument of the previous service provider.", sp.Name, input.Name, input.Description));
                });

                foreach (var kvp in sp.ContractOutputs.Where(output => output.IsRequired).ToList())
                    simulation.Add(kvp.Name);
            }
        }

        /// <summary>
        /// Executes the pipeline for each service provider in the workflow
        /// </summary>
        /// <exception cref="OperationCanceledException"></exception>
        internal void Execute()
        {
            var model = new WorkflowModel(); //Initialize data transfer between service providers

            foreach (var sp in Workflow)
            {
                model.Changelog.Add(new ChangelogModel(sp)); //Tag each action

                try
                {
                    sp.ProcessPipeline(model); //Execute the current pipeline operation

                    //Validate that the pipeline respected the output contract
                    foreach (var kvp in sp.ContractOutputs.Where(output => output.IsRequired).ToList())
                        if (!model.Values.Keys.Any(key => kvp.Name == key))
                            throw new OperationCanceledException(string.Format("Workflow was cancelled because '{0}' didn't respect it's required output contract. Key '{1}' ({2}) not found", sp, kvp.Name, kvp.Description));
                }
                catch (Exception ex)
                {

                    model.Exception = ex;
                    throw new OperationCanceledException(string.Format("Workflow was cancelled because '{0}' threw an exception", sp.Name), ex);
                }
            }
        }

    }
}
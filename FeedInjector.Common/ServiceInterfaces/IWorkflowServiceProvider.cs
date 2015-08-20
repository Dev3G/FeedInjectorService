using FeedInjector.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.ServiceInterfaces
{
    /// <summary>
    /// A service provider that interacts with objects in a workflow
    /// </summary>
    public interface IPipelineServiceProvider
    {
        /// <summary>
        /// Contract name of the service
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Brief description of the pipeline service
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Parameters specified in the service call
        /// </summary>
        Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Executes an action over an object in the pipeline
        /// </summary>
        /// <param name="model"></param>
        void ProcessPipeline(WorkflowModel model);

        /// <summary>
        /// Specifies the required and optional input parameters
        /// </summary>
        List<ServiceParameterModel> ContractInputs { get; }
        
        /// <summary>
        /// Specifies the required and optional output parameters
        /// </summary>
        List<ServiceParameterModel> ContractOutputs { get; }
    }
}

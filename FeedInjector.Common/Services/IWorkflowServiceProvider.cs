﻿using FeedInjector.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Services
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
        /// Executes an action over an object in the pipeline
        /// </summary>
        /// <param name="model"></param>
        void ProcessPipeline(WorkflowModel model);
    }
}

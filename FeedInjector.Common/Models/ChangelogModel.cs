using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    /// <summary>
    /// Describes action performed by a service
    /// </summary>
    public class ChangelogModel
    {
        /// <summary>
        /// Name of the service that performed the action
        /// </summary>
        public string PipelineServiceName { get; private set; }

        /// <summary>
        /// Description of the action performed by the service
        /// </summary>
        public string PipelineServiceAction { get; private set; }

        /// <summary>
        /// When the action was executed
        /// </summary>
        public DateTime Timestamp { get; private set; }

        public ChangelogModel(string provider, string action )
        {
            this.PipelineServiceName = provider;
            this.PipelineServiceAction = action;
            this.Timestamp = DateTime.Now;
        }
        public ChangelogModel(IPipelineServiceProvider sp)
        {
            this.PipelineServiceName = sp.Name;
            this.PipelineServiceAction = sp.Description;
            this.Timestamp = DateTime.Now;
        }
    }
}

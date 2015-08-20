using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    /// <summary>
    /// Provides a standard communication object along the executing workflow
    /// </summary>
    public class WorkflowModel
    {
        /// <summary>
        /// Stores data through the workflow
        /// </summary>
        public Dictionary<string,string> Values { get; set; }

        /// <summary>
        /// Logs every service provider action through the workflow
        /// </summary>
        public List<ChangelogModel> Changelog { get; set; }

        /// <summary>
        /// Stores an exception for later use
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets the exeution date of the workflow
        /// </summary>
        public DateTime ExecutionDate { get; private set; }

        /// <summary>
        /// Initializes the workflow model
        /// </summary>
        public WorkflowModel()
        {
            this.Values = new Dictionary<string, string>();
            this.Changelog = new List<ChangelogModel>();
            this.ExecutionDate = DateTime.Now;
        }
    }
}

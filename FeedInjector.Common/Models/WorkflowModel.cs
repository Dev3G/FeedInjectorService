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
        /// Gets or sets a data object to be available to all the pipeline service providers in the chain during the workflow execution
        /// </summary>
        /// <param name="key">The key to the object</param>
        /// <returns></returns>
        public object this[string key] 
        {
            get
            {
                return Values.ContainsKey(key) ? Values[key] : null;
            }
            set
            {
                if (Values.ContainsKey(key))
                    Values[key] = value;
                else
                    Values.Add(key, value);
            }
        }

        /// <summary>
        /// Key based data object that is expanded through the workflow
        /// </summary>
        public Dictionary<string, object> Values { get; private set; }

        /// <summary>
        /// Logs every service provider action through the workflow
        /// </summary>
        public List<ChangelogModel> Changelog { get; private set; }

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
            this.Values = new Dictionary<string, object>();
            this.Changelog = new List<ChangelogModel>();
            this.ExecutionDate = DateTime.Now;
        }
    }
}

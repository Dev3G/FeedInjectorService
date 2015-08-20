using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    /// <summary>
    /// Contains the information necessary to execute a <typeparamref name="IPipelineParameterModel"/>
    /// </summary>
    public class ServiceParameterModel
    {
        public ServiceParameterModel(string name, bool required, string description = null)
        {
            this.Name = name;
            this.IsRequired = required;
            this.Description = description ?? string.Empty;
        }

        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the parameter
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether the parameter is required or not
        /// </summary>
        public bool IsRequired { get; set; }
    }
}

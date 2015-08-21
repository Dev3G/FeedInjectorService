using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    [AttributeUsage(System.AttributeTargets.Class,AllowMultiple=true)]
    /// <summary>
    /// Contains the information necessary to execute a <typeparamref name="IPipelineParameterModel"/>
    /// </summary>
    public class ServiceParameterAttribute : System.Attribute
    {
        public ServiceParameterAttribute(ServiceParameterType type, string name, bool isRequired, string description = null)
        {
            this.Name = name;
            this.IsRequired = isRequired;
            this.Description = description ?? string.Empty;
            this.Type = type;
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

        /// <summary>
        /// Whether it's an input or an output parameter
        /// </summary>
        public ServiceParameterType Type { get; set; }
    }

    public enum ServiceParameterType
    {
        Input,
        Output
    }
}

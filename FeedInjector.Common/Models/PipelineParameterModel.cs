using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    public class PipelineParameterModel
    {
        public PipelineParameterModel(string name, bool required, string description = null)
        {
            this.Name = name;
            this.Required = required;
            this.Description = description ?? string.Empty;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }
    }
}

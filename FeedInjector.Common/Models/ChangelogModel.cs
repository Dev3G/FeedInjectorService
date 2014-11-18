using FeedInjector.Common.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedInjector.Common.Models
{
    public class ChangelogModel
    {
        public string PipelineProvider { get; set; }

        public string Action { get; set; }

        public DateTime Timestamp { get; set; }
        public ChangelogModel(string provider, string action )
        {
            this.PipelineProvider = provider;
            this.Action = action;
            this.Timestamp = DateTime.Now;
        }
        public ChangelogModel(IPipelineServiceProvider sp)
        {
            this.PipelineProvider = sp.ServiceName;
            this.Action = sp.ServiceDescription;
            this.Timestamp = DateTime.Now;
        }
    }
}

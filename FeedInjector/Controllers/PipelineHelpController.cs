using FeedInjector.Common.Models;
using FeedInjector.Infrastructure;
using FeedInjector.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace FeedInjector.Controllers
{
    public class PipelineHelpController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Help";

            var container = new ServiceProviderCompositionContainer();
            var providers = container.GetAllProviders();

            var response = new StringBuilder();

            var provlist = new List<ExpandoObject>();


            foreach (var p in providers)
            {
                var prov = new { Name = p.Name, Description = p.Description, Parameters = new List<ServiceParameterAttribute>() };
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Input).Where(i => i.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Input).Where(i => !i.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Output).Where(i => i.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Output).Where(i => !i.IsRequired).ToList());
                provlist.Add(ToExpando(prov));

                response.AppendLine(string.Format("<h3>{0}</h3><markdown>{1}</markdown>", p.Name, p.Description));

                response.AppendLine("<ul>");
                foreach (var param in p.GetServiceParameters(ServiceParameterType.Input).Where(i => i.IsRequired).ToList())
                    response.AppendLine(string.Format("<li><strong>[Input,Required]</strong> {0}: {1}</li>", param.Name, param.Description));
                
                foreach (var param in p.GetServiceParameters(ServiceParameterType.Input).Where(i => !i.IsRequired).ToList())
                    response.AppendLine(string.Format("<li><strong>[Input,Optional]</strong> {0}: {1}</li>", param.Name, param.Description));
                
                foreach (var param in p.GetServiceParameters(ServiceParameterType.Output).Where(i => i.IsRequired).ToList())
                    response.AppendLine(string.Format("<li><strong>[Output,Required]</strong> {0}: {1}</li>", param.Name, param.Description));
                
                foreach (var param in p.GetServiceParameters(ServiceParameterType.Output).Where(i => !i.IsRequired).ToList())
                    response.AppendLine(string.Format("<li><strong>[Output,Optional]</strong> {0}: {1}</li>", param.Name, param.Description));
                
                response.AppendLine("</ul>");

            }

            ViewBag.Controllers = response.ToString();

            return View(provlist);
        }

        private static ExpandoObject ToExpando(object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var obj = propertyDescriptor.GetValue(anonymousObject);
                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }
    }
}

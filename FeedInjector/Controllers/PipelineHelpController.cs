using FeedInjector.Common.Models;
using FeedInjector.Infrastructure;
using FeedInjector.Infrastructure.Extensions;
using FeedInjector.Common.Infrastructure;
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
                var call = p.Name + "(";
                int i = 1;
                foreach (var input in p.GetServiceParameters(ServiceParameterType.Input))
                    call += string.Format("{0}: val{1},", input.Name, i++);
                call = call.TrimEnd(',');
                call = call.EndsWith("(") ? call.TrimEnd('(') : call + ")";

                var prov = new { Name = p.Name, Description = p.Description, ExampleCall = call, Parameters = new List<ServiceParameterAttribute>() };
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Input).Where(input => input.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Input).Where(input => !input.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Output).Where(input => input.IsRequired).ToList());
                prov.Parameters.AddRange(p.GetServiceParameters(ServiceParameterType.Output).Where(input => !input.IsRequired).ToList());

                
                provlist.Add(ToExpando(prov));
            }

            

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

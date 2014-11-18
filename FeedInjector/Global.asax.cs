using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FeedInjector.Controllers.Factories;
using System.Web.Http.Dispatcher;

namespace FeedInjector
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var currentWebApiActivator = GlobalConfiguration.Configuration.Services.GetHttpControllerActivator();
            //var mefFactory = new MefControllerFactory(container, currentWebApiActivator);
            //ControllerBuilder.Current.SetControllerFactory(mefFactory);

            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), mefFactory);
        }
    }
}

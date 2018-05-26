using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CoffeeTest.Data.DBContext;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace CoffeeTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static WindsorContainer Container { get; set; }

        protected void Application_Start()
        {

            Database.SetInitializer(new CoffeeTestInitializer());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


    }
}

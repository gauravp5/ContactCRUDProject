using Autofac;
using Autofac.Integration.Mvc;
using ContactManager.Core.IRepositories;
using ContactManager.Core.Services;
using ContactManager.Data;
using ContactManager.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRUD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = RegisterDependenciesWithIoCContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static Autofac.IContainer RegisterDependenciesWithIoCContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ContactManagerService>().As<IContactManagerService>().InstancePerRequest();

            builder.RegisterType<ContactRepository>()
                .UsingConstructor(typeof(ContactManagerContext))
                .As<IContactRepository>().InstancePerRequest();

            builder.RegisterType<ContactManagerContext>().InstancePerRequest();
            var container = builder.Build();
            return container;
        }
    }
}
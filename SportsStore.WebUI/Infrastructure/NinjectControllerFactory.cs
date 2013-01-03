using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using System.Web.Routing;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System.Configuration;
using SportsStore.Domain.Services;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances 
        IKernel kernel = new StandardKernel(new SportsStoreServices());

        // ASP.NET MVC calls this to get the controller for each request 
        protected override IController GetControllerInstance(RequestContext context, Type controllerType) 
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class SportsStoreServices : NinjectModule 
        {
            public override void Load()
            {
                Bind<IProductsRepository>()
                    .To<SqlProductsRepository>()
                    .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString);
                Bind<IOrderSubmitter>().To<EmailOrderSubmitter>().WithConstructorArgument(
                    "mailTo",
                    ConfigurationManager.AppSettings["EmailOrderSubmitter.MailTo"]
                );
            }
        }
    }
}
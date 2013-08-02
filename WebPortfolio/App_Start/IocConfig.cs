using WebPortfolio.Core.Repositories;
using WebPortfolio.Ninject;
using WebPortfolio.Repositories;
using Ninject;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebPortfolio
{
    public static class IocConfig
    {

        public static void RegisterIoc(HttpConfiguration config)
        {
            var kernel = new StandardKernel();

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));


            kernel.Bind(typeof(IWPRepository<>)).To(typeof(WPRepository<>));
            kernel.Bind(typeof(IDbRepository)).To(typeof(DbRepository));

            kernel.Components.Add<IInjectionHeuristic, NinjectInjectionHeuristic>();

            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

    }
}
using Ninject;
using System;
using System.Web.Mvc;

namespace WebPortfolio.Ninject
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;
        public NinjectControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
            //Return MyBase.GetControllerInstance(requestContext, controllerType)
        }
    }
}

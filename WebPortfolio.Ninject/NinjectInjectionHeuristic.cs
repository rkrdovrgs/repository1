using Ninject;
using Ninject.Components;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebPortfolio.Ninject
{
    public class NinjectInjectionHeuristic : NinjectComponent, IInjectionHeuristic
    {
        private readonly IKernel kernel;

        private static readonly string[] knownNameSpaces = new string[] { "WebPortfolio.Core.Repositories" };
        public NinjectInjectionHeuristic(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool ShouldInject(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            return ShouldInject(propertyInfo);
        }

        private bool ShouldInject(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                return false;
            }

            if (!propertyInfo.CanWrite)
            {
                return false;
            }

            Type propertyType = propertyInfo.PropertyType;
            string propNameSpace = propertyType.Namespace;
            if (!knownNameSpaces.Contains(propNameSpace))
            {
                return false;
            }

            var instance = kernel.TryGet(propertyType);
            return instance != null;
        }
    }
}

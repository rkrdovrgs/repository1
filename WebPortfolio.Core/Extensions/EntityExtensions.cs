using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Core.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsNullOrEmpty<T>(this T obj)
            where T : IEntity
        {
            if (obj == null)
                return true;


            var type = typeof(T);
            foreach (var p in type.GetProperties().Where(x => !(x.Name.EndsWith("Id") && Nullable.GetUnderlyingType(x.PropertyType) != null)))
            {
                var t = p.PropertyType;
                if (t.GetInterfaces().Contains(typeof(IEntity)))
                {
                    if (!((IEntity)p.GetValue(obj)).IsNullOrEmpty())
                        return false;
                }
                else
                    switch (t.Name.ToLower())
                    {
                        case "string":
                            if (!string.IsNullOrEmpty((string)p.GetValue(obj)))
                                return false;
                            break;
                        default:
                            if (p.GetValue(obj) != null)
                                return false;
                            break;
                    }
            }

            return true;
        }
    }
}

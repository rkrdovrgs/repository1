using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace WebPortfolio.Repositories
{

    public class RepositoryBase<C> : IDisposable
        where C : DbContext, new()
    {
        private C _DataContext;

        public virtual C DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = new C();
                    this.AllowSerialization = true;
                    //Disable ProxyCreationDisabled to prevent the "In order to serialize the parameter, add the type to the known types collection for the operation using ServiceKnownTypeAttribute" error
                }
                return _DataContext;
            }
        }

        public virtual bool AllowSerialization
        {
            get
            {
                //return ((IObjectContextAdapter) _DataContext)
                //.ObjectContext.ContextOptions.ProxyCreationEnabled = false;
                return _DataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _DataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }

        //public bool SubmitChanges()
        //{
        //    var status = DataContext.SaveChanges() > 0;
        //    Dispose();
        //    _DataContext = new C();
        //    return status;

        //}
    }
}
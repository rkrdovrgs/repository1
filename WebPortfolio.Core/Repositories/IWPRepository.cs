using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Core.Repositories
{
    public interface IWPRepository<T> : IRepository<T>
        where T : class
    {
    }
}

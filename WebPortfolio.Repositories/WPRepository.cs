
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models;

namespace WebPortfolio.Repositories
{
    public class DebtRepository<T> : Repository<T, WebPortfolioEntities>, IWPRepository<T>
        where T: class
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebPortfolio.Models.Entities.Configuration
{
    public partial class WebPortfolioEntities : WebPortfolio.Models.Entities.WebPortfolioEntities
    {
        //public WebPortfolioEntities()
        //    : this(false) { }

        //public WebPortfolioEntities(bool proxyCreationEnabled)
        //    : base() { }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<WebPortfolioEntities>(null);
        }
    }
}

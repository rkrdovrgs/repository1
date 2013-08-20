using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;

namespace WebPortfolio.Controllers.Api
{
    public class CountryController : ApiController
    {   
        public IWPRepository<Country> countryrepository { get; set; }
        // GET api/country
        public IEnumerable<Country> GetValues()
        {
            return countryrepository.GetList();
        }

       
    }
}

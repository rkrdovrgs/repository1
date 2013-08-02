using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;

namespace WebPortfolio.Controllers
{
    public class UserProfilesController : Controller
    {

        public IWPRepository<UserProfile> userprofilerepository { get; set; }
        

        public ActionResult Index()
        {
            var model = userprofilerepository.GetList();
            return View(model);
        }

    }
}

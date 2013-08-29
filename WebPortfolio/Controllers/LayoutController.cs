using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;

namespace WebPortfolio.Controllers
{
    public class LayoutController : Controller
    {
        public IWPRepository<UserProfile> userprofilerepository { get; set; }
                
       
        public PartialViewResult ProfilePicture()
        {
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName)
                                                   .Include(x => x.File)
                                                   .FirstOrDefault();

            return PartialView("_ProfilePicture", userProfile);
        }


    }
}

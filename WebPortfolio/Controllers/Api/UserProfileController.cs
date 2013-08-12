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
    public class UserProfileController : ApiController
    {

        public IWPRepository<UserProfile> userprofilerepository { get; set; }

        // GET api/userprofile
        [HttpGet]
        public UserProfile Details()
        {
            var userName = User.Identity.Name;
            return userprofilerepository.Get(x => x.UserName == userName);
        }

        
        

        // POST api/userprofile
        public void Post([FromBody]string value)
        {
        }

        // PUT api/userprofile/5
        public HttpResponseMessage Put(int id, [FromBody]UserProfile userProfile)
        {
            userprofilerepository.Update(userProfile);

            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}

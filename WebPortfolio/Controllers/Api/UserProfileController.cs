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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userprofile/5
        public UserProfile Get(int id)
        {
            return userprofilerepository.Get(x => x.UserId == id);
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

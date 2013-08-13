using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;

namespace WebPortfolio.Controllers.Api
{
    public class UserProfileController : ApiController
    {

        public IWPRepository<UserProfile> userprofilerepository { get; set; }
        public IWPRepository<UserAddress> useraddressrepository { get; set; }
        public IWPRepository<UserPhone> userphonerepository { get; set; }
        
        // GET api/userprofile
        [HttpGet]
        public UserProfile Details()
        {
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName).Include (x => x.UserAddresses ).Include(x => x.UserPhones).FirstOrDefault();

            if (userProfile.UserAddresses == null || !userProfile.UserAddresses.Any())
                userProfile.UserAddresses = new List<UserAddress>() { new UserAddress{
                UserId = userProfile.UserId
                } };
            
            return userProfile;
        }


        // POST api/userprofile
        public void Post([FromBody]string value)
        {
        }


        // PUT api/userprofile/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody]UserProfile userProfile)
        {
            //var username = User.Identity.Name;
            //var userid = userprofilerepository.Get(x => x.UserName == username ) ;
            userprofilerepository.Update(userProfile);


            if(userProfile.UserAddresses != null && userProfile.UserAddresses.Any()) //si useraddress no es nulo y si contiene una fila
                if(userProfile.UserAddresses.FirstOrDefault().UserAddressId == 0)
                    useraddressrepository.Save(userProfile.UserAddresses.FirstOrDefault(),true);
                else
                    useraddressrepository.Update(userProfile.UserAddresses.FirstOrDefault());

            if (userProfile.UserPhones != null && userProfile.UserPhones.Any())
                userphonerepository.Update(userProfile.UserPhones.FirstOrDefault());
            
            return new HttpResponseMessage(HttpStatusCode.OK) { 
            
            };

        }

        //[HttpPut]
        //public HttpResponseMessage Put([FromBody]UserAddress userAddress)
        //{
        //    //var username = User.Identity.Name;
        //    //var userid = userprofilerepository.Get(x => x.UserName == username ) ;
        //    useraddressrepository.Update(userAddress);

        //    return new HttpResponseMessage(HttpStatusCode.OK);

        //}

        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}

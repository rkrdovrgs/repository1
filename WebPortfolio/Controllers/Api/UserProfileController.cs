using System.Linq;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;
using WebPortfolio.Core.Extensions;
using System.Collections.Generic;
using System.Web;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Controllers.Api
{
    public class UserProfileController : ApiController
    {

        public IWPRepository<UserProfile> userprofilerepository { get; set; }
        public IWPRepository<UserAddress> useraddressrepository { get; set; }
        public IWPRepository<UserPhone> userphonerepository { get; set; }
        public IWPRepository<Country> countryrepository { get; set; }
        public IWPRepository<File> filesrepository { get; set; }

        // GET api/userprofile
        [HttpGet]
        public UserProfile Details()
        {
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName)
                                                   .Include(x => x.UserAddress)
                                                   .Include(x => x.UserPhones)
                                                   .Include(x => x.Picture)
                                                   .FirstOrDefault();

            return userProfile;
        }

        // PUT api/userprofile/5
        [HttpPut]
        public object Put([FromBody]UserProfile userProfile)
        {
            if (userProfile.UserAddress != null)
                userProfile.UserAddress.UserId = userProfile.Id;

            if (userProfile.UserAddress.IsNullOrEmpty())
            {
                useraddressrepository.Delete(userProfile.UserAddress);
                userProfile.UserAddress = null;
            }


            var dbPhones = userphonerepository.GetList(x => x.UserId == userProfile.Id).ToList();

            userProfile.UserPhones = userProfile.UserPhones
                                            .Where(x => x.Number != 0)
                                            .GroupBy(x => x.Number)
                                            .Select(x => userProfile.UserPhones.First(p => p.Number == x.Key))
                                            .ToList();

            var missingPhones = dbPhones.Where(x => !userProfile.UserPhones.Any(n => n.Id == x.Id)).ToList();
            userphonerepository.DeleteCollection(missingPhones);



            //if (userProfile.UserAddress.CountryId != null)
            //{ 

            //}




            userprofilerepository.InsertOrUpdate(userProfile);

            return new
            {
                userAddressId = userProfile.UserAddress == null ? 0 : userProfile.UserAddress.Id,
                userPhones = userphonerepository.GetList(x => x.UserId == userProfile.Id)
            };



        }


        [HttpPut]
        public object Picture([FromBody]File fileInfo)
        { 
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.Get(x => x.UserName == userName);

            userProfile.PictureId = fileInfo.Id;

            userprofilerepository.Update(userProfile);

            userProfile.Picture = new File { 
                Id = fileInfo.Id,
                Name = fileInfo.Name
            };

            return new { pictureUrl = userProfile.PictureUrl };
        }

    }
}

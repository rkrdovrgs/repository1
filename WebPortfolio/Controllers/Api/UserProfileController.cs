using System.Linq;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;
using WebPortfolio.Core.Extensions;

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
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName)
                                    .Include(x => x.UserAddress)
                                    .Include(x => x.UserPhones)
                                    .FirstOrDefault();

            return userProfile;
        }


        // POST api/userprofile
        public void Post([FromBody]string value)
        {
        }


        // PUT api/userprofile/5
        [HttpPut]
        public object Put([FromBody]UserProfile userProfile )
        {
            var isNewAddress = userProfile.UserAddress != null && userProfile.UserAddress.UserId == 0; //la tabla address no es nula y no hay un registro. se guardara por primera vez
            var isnewphone = true;            var comparephone = false;
            int[] userNumbers = userProfile.UserPhones.Select(x => x.Number).ToArray();
            int nextphone = userNumbers[1];
            for (int i = 0; i < userNumbers.Length; i++ )
            {                
                if (userNumbers[i] == nextphone)
                {                   
                    comparephone = true;
                    
                }
                nextphone = userNumbers[i + 2];
            }
            
            
            
            

            if (isNewAddress)
                userProfile.UserAddress.UserId = userProfile.UserId;
            //if (isNewPhone)
            //    userProfile.UserPhone.UserId = userProfile.UserId;

            //var username = User.Identity.Name;
            //var userid = userprofilerepository.Get(x => x.UserName == username ) ;
            userprofilerepository.Update(userProfile);


            ///TODO: Validar si la direccion esta vacia
            ///Si esta vacia y existe, borrar
            ///Sino si esta vacia, no guardar
            ///Sino, guardar o actualizar
            
           
            //Estableciendo en SavePutDelete en UserAddress       
            if (isNewAddress)     
                //userProfile.UserAddress.UserId = userProfile.UserId;
                useraddressrepository.Save(userProfile.UserAddress);  

            else if (userProfile.UserAddress.Line1.IsNullOrEmpty() && userProfile.UserAddress.Line2.IsNullOrEmpty() && userProfile.UserAddress.State.IsNullOrEmpty() && userProfile.UserAddress.City.IsNullOrEmpty() && !userProfile.UserAddress.Zipcode.HasValue )
            {
                useraddressrepository.Delete(userProfile.UserAddress);
                return new { userAddressId = 0 };                
            }

            else
                useraddressrepository.Update(userProfile.UserAddress);

            //Estableciendo en SavePutDelete en UserPhone 
            //if (isNewPhone)
            //    userphonerepository.Save(userProfile.UserPhone);

            //else if (userProfile.UserPhone.Number == null)
            //{
            //    userphonerepository.Delete(userProfile.UserPhone);
            //    return new { userPhoneId = 0 };
            //}

            //else
            //    userphonerepository.Update(userProfile.UserPhone);


            return new
            {
                userAddressId = userProfile.UserAddress.UserId
            };

            //else if (!userProfile.UserAddresses.Any())
            //    useraddressrepository.Delete(userProfile.UserAddresses.FirstOrDefault());
            ////if (userProfile.UserPhones != null && userProfile.UserPhones.Any())
            ////    userphonerepository.Update(userProfile.UserPhones.FirstOrDefault());
            
           

        }


        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}

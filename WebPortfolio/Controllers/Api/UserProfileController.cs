using System.Linq;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;
using WebPortfolio.Core.Extensions;
using System.Collections.Generic;
using System.Web;

namespace WebPortfolio.Controllers.Api
{
    public class UserProfileController : ApiController
    {

        public IWPRepository<UserProfile> userprofilerepository { get; set; }
        public IWPRepository<UserAddress> useraddressrepository { get; set; }
        public IWPRepository<UserPhone> userphonerepository { get; set; }

        public IWPRepository<Country> countryrepository { get; set; }
        public IWPRepository<File> filesrepository { get; set; }

        public UserProfile Get(int id)
        {
            return userprofilerepository.Get(id);
        }

        // GET api/userprofile
        [HttpGet]
        public UserProfile Details()
        {
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName)
                                    .Include(x => x.UserAddress)
                                    .Include(x => x.UserPhones)   
                                    .Include(x=> x.File)
                                    .FirstOrDefault();
            //if (userProfile.UserAddress.CountryId.HasValue)
            //    userProfile.UserAddress.Country = countryrepository.Get((int)userProfile.UserAddress.CountryId);
            return userProfile;
        }


        // POST api/userprofile
        public void Post([FromBody]string value)
        {
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
            
            //var isNewAddress = userProfile.UserAddress != null && userProfile.UserAddress.UserId == 0; //la tabla address no es nula y no hay un registro. se guardara por primera vez

            //var username = User.Identity.Name;
            //var userid = userprofilerepository.Get(x => x.UserName == username ) ;


            ///TODO: Validar si la direccion esta vacia
            ///Si esta vacia y existe, borrar
            ///Sino si esta vacia, no guardar
            ///Sino, guardar o actualizar


            //Estableciendo en SavePutDelete en UserAddress       
            //if (isNewAddress)
            //{
            //userProfile.UserAddress.UserId = userProfile.Id;
            //TODO: CHANGE TO INSERT
            //useraddressrepository.Save(userProfile.UserAddress);
            //}

            //else if (userProfile.UserAddress.Line1.IsNullOrEmpty() && userProfile.UserAddress.Line2.IsNullOrEmpty() && userProfile.UserAddress.State.IsNullOrEmpty() && userProfile.UserAddress.City.IsNullOrEmpty() && !userProfile.UserAddress.Zipcode.HasValue)
            //{
            // useraddressrepository.Delete(userProfile.UserAddress);
            //return new { userAddressId = 0 };
            //}

            //else
            //useraddressrepository.Update(userProfile.UserAddress);
            // end UserAddress


            //Estableciendo en SavePutDelete en UserPhone 
            //var oldListPhone = userProfile.UserPhones.Select(x => x).Where(x => x.Id == x.Id).ToList();// lista todos los numeros, viejos y nuevos
            //var newListPhone = oldListPhone.ToList();
            //var distintcPhone = newListPhone.Count() == userProfile.UserPhones.Select(x => x.Number).Distinct().Count();//diferencia entre el total de registro y el total de numeros diferentes, deben de coincidir, sino, es que hay registro duplicados
            //int con = 0;
            //if (distintcPhone)// si ningun numero de telefono se repite..
            //{
            //    foreach (var item in oldListPhone)
            //    {
            //        if (item.Number == 0)// si no contiene numero de telefono "B"..
            //        {
            //            if (item.Id != 0)//si ya exite el registro y se borro el numero.. se elimina de la base de datos                            
            //                userphonerepository.Delete(item);
            //        }
            //        else
            //        {
            //            newListPhone.RemoveAt(con);
            //            con--;
            //        }
            //        con++;
            //    }
            //    userphonerepository.InsertOrUpdateCollection(newListPhone);
            //}


            //foreach (var p in userProfile.UserPhones)
            //{
            //    p.UserId = userProfile.Id;
            //}

            var dbPhones = userphonerepository.GetList(x => x.UserId == userProfile.Id).ToList();

            userProfile.UserPhones = userProfile.UserPhones
                                            .Where(x => x.Number != 0)
                                            .GroupBy(x => x.Number)
                                            .Select(x => userProfile.UserPhones.First(p => p.Number == x.Key))
                                            .ToList();

            var missingPhones = dbPhones.Where(x => !userProfile.UserPhones.Any(n => n.Id == x.Id)).ToList();
            userphonerepository.DeleteCollection(missingPhones);



            if (userProfile.UserAddress.CountryId != null)
            { 

            }

            //userphonerepository.InsertOrUpdateCollection(userProfile.UserPhones);


            //else: aviso, no puede tener dos telefonos con el mismo numero         
            // END SavePutDelete UserPhone

            //userprofilerepository.SubmitChanges();

            
            userprofilerepository.InsertOrUpdate(userProfile);

            return new
            {
                userAddressId = userProfile.UserAddress == null ? 0 : userProfile.UserAddress.Id,
                userPhones = userphonerepository.GetList(x => x.UserId == userProfile.Id)
            };

            //else if (!userProfile.UserAddresses.Any())
            //    useraddressrepository.Delete(userProfile.UserAddresses.FirstOrDefault());
            ////if (userProfile.UserPhones != null && userProfile.UserPhones.Any())
            ////    userphonerepository.Update(userProfile.UserPhones.FirstOrDefault());
            //var country = userProfile.UsernewListPhone[1].Number != 0Address.Country.; 


        }

       
        // DELETE api/userprofile/5
        public void Delete(int id)
        {
        }
    }
}

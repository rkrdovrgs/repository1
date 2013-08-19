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
        public IWPRepository<Country> countryrepository { get; set; }


        // GET api/userprofile
        [HttpGet]
        public UserProfile Details()
        {
            var userName = User.Identity.Name;
            var userProfile = userprofilerepository.GetList(x => x.UserName == userName)
                                    .Include(x => x.UserAddress)
                                    .Include(x => x.UserPhones)
                                    .Include(x => x.UserAddress.Country)
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
            int _userAddressId = 0;
            if (!userProfile.UserAddress.IsNullOrEmpty())
            {
                userProfile.UserAddress.UserId = userProfile.Id;
                useraddressrepository.InsertOrUpdate(userProfile.UserAddress);
                _userAddressId = userProfile.UserAddress.Id;
            }
            else
                useraddressrepository.Delete(userProfile.UserAddress);
            

            //var isNewAddress = userProfile.UserAddress != null && userProfile.UserAddress.UserId == 0; //la tabla address no es nula y no hay un registro. se guardara por primera vez
              
            //var username = User.Identity.Name;
            //var userid = userprofilerepository.Get(x => x.UserName == username ) ;
            userprofilerepository.Update(userProfile);          

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
            if (false)
            {
                var oldListPhone = userProfile.UserPhones.Select(x => x).Where(x => x.Id == x.Id).ToList();// lista todos los numeros, viejos y nuevos
                var newListPhone = oldListPhone.ToList();
                int con = 0;
                foreach (var item in oldListPhone)
                {
                    if (item.Number == 0)// si no contiene numero de telefono 
                    {
                        if (item.Id != 0)//si ya exite un registro y se borro el numero. se elimina de la base de datos
                        {
                            userphonerepository.Delete(item);
                        }
                        newListPhone.RemoveAt(con); // se remueve el registro que no contiene numero de telefono
                        con--; // se descrementa porque cuando se elimina un registro de una lista, los que tienen posiciones superiores bajan un 'escalon' 
                    }
                    con++;
                }

                var distintcPhone = newListPhone.Count() == newListPhone.Distinct().Count();//diferencia entre el total de registro y el total de numeros diferentes, deben de coincidir, sino, es que hay registro duplicados
                //var isNewPhone = newListPhone.Select(x => x).Where(x => x.Id == 0).Count();
                var saveNewPhone = newListPhone.Select(x => x).Where(x => x.Id == 0).ToList();
                var newPhones = saveNewPhone.Count();

                if (newListPhone.Count() != 0 && distintcPhone)// si hay registros se actualizan
                {
                    for (int i = 0; i < newListPhone.Count(); i++)
                    {
                        if (newListPhone[i].Id != 0)
                            userphonerepository.Update(newListPhone[i]);
                    }
                }

                if (newPhones != 0 && distintcPhone)// si hay un nuevo phone y si no existe ese numero
                {
                    for (int i = 0; i < newPhones; i++)
                    {
                        //TODO: CHANGE TO INSERT
                        //userphonerepository.Save(saveNewPhone[i]);
                    }
                }
            }
            // end the phone
            //userprofilerepository.SubmitChanges();

            return new
            {
                userAddressId = _userAddressId,
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

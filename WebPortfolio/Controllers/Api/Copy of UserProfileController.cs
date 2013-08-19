using System.Linq;
using System.Web.Http;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;
using System.Data.Entity;
using WebPortfolio.Core.Extensions;

namespace WebPortfolio.Controllers.Api
{
    public class UserProfileController2 : ApiController
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
            var isNewAddress = userProfile.UserAddress != null && userProfile.UserAddress.UserId == 0; //la tabla address no es nula y no hay un registro. se guardara por primera vez
              
            //var username = User.Identity.Name;
            //var userid = userprofilerepository.Get(x => x.UserName == username ) ;
            userprofilerepository.Update(userProfile);          

            ///TODO: Validar si la direccion esta vacia
            ///Si esta vacia y existe, borrar
            ///Sino si esta vacia, no guardar
            ///Sino, guardar o actualizar
            
           
            //Estableciendo en SavePutDelete en UserAddress       
            if (isNewAddress)
            {
                userProfile.UserAddress.UserId = userProfile.Id;
                //TODO: CHANGE TO INSERT
                //useraddressrepository.Save(userProfile.UserAddress);
            }

            else if (userProfile.UserAddress.Line1.IsNullOrEmpty() && userProfile.UserAddress.Line2.IsNullOrEmpty() && userProfile.UserAddress.State.IsNullOrEmpty() && userProfile.UserAddress.City.IsNullOrEmpty() && !userProfile.UserAddress.Zipcode.HasValue)
            {
                useraddressrepository.Delete(userProfile.UserAddress);
                return new { userAddressId = 0 };
            }

            else
                useraddressrepository.Update(userProfile.UserAddress);
            // end UserAddress


            //Estableciendo en SavePutDelete en UserPhone 
            var oldListPhone = userProfile.UserPhones.Select(x => x).Where(x => x.Id == x.Id).ToList();// lista todos los numeros, viejos y nuevos
            var newListPhone = oldListPhone.ToList() ;
            var distintcPhone = newListPhone.Count() == userProfile.UserPhones.Select(x => x.Number).Distinct().Count();//diferencia entre el total de registro y el total de numeros diferentes, deben de coincidir, sino, es que hay registro duplicados
            
            var saveNewPhone = newListPhone.Select(x => x).Where(x => x.Id == 0).ToList(); //obtiene todos los items cuyo id = 0, osea que son nuevos(el usuario lo acaba de agregar)
            int con = 0;

            if (distintcPhone)// si ningun numero de telefono se repite..
            {
                foreach (var item in oldListPhone)
                {                    
                    if (item.Id != 0 && item.Number != 0) //si el registro contiene un id diferente de 0(se refiere a q el registro esta almacenado en la DB) y contiene un numero de telefono, se actualiza
                        userphonerepository.Update(newListPhone[con]);

                    else if (item.Id == 0 &&  item.Number != 0) //si es un registro nuevo y contiene un numero de telefono, guarda.
                        //TODO: CHANGE TO INSERT
                        // userphonerepository.Save(saveNewPhone[i]);

                    if (item.Number == 0)// si no contiene numero de telefono "B"..
                    {
                        if (item.Id != 0)//si ya exite el registro y se borro el numero.. se elimina de la base de datos                            
                            userphonerepository.Delete(item);
                            
                        newListPhone.RemoveAt(con); //B: se remueve de la lista el registro que no contiene numero de telefono
                        con--; // se descrementa porque cuando se elimina un registro de una lista, los que tienen posiciones superiores bajan un 'escalon' 
                    }
                    con++;                           
                }
            }
            //else: aviso, no puede tener dos telefonos con el mismo numero         
            // end SavePutDelete UserPhone

            //userprofilerepository.SubmitChanges();
            return new
            {
                userAddressId = userProfile.UserAddress.UserId,
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

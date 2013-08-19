using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Controllers.Api;
using Moq;
using WebPortfolio.Models.Entities;
using WebPortfolio.Core.Repositories;

namespace WebPortfolio.Tests.Controllers
{
    public class UserProfileControllerTests
    {
        [TestClass]
        public class Put_Debe
        {
          
            [TestMethod]
            public void llamar_addressreposity_delete_si_la_direccion_existe_y_esta_vacia()
            {
                //Arrange
                var userProfileController = new UserProfileController();

                var useraddressrepoMock = new Mock<IWPRepository<UserAddress>>();
                var userprofilerepoMock = new Mock<IWPRepository<UserProfile>>();
                
                userProfileController.useraddressrepository = useraddressrepoMock.Object;
                userProfileController.userprofilerepository = userprofilerepoMock.Object;

                var userProfile = new UserProfile
                {
                    UserAddress = new UserAddress { 
                        Id = 123
                    }
                };

                //Act
                userProfileController.Put(userProfile);

                
                //Assert
                useraddressrepoMock.Verify(x => x.Delete(It.IsAny<UserAddress>()));               

                
            }


            [TestMethod]
            public void llamar_addressreposity_update_si_la_direccion_existe_y_esta_llena()
            {
                //Arrange
                var userProfileController = new UserProfileController();

                var useraddressrepoMock = new Mock<IWPRepository<UserAddress>>();
                var userprofilerepoMock = new Mock<IWPRepository<UserProfile>>();

                userProfileController.useraddressrepository = useraddressrepoMock.Object;
                userProfileController.userprofilerepository = userprofilerepoMock.Object;

                var userProfile = new UserProfile
                {
                    UserAddress = new UserAddress
                    {
                        Id = 123,
                        State= "ho"
                    }
                };

                //Act
                userProfileController.Put(userProfile);


                //Assert
                useraddressrepoMock.Verify(x => x.Update(It.IsAny<UserAddress>()));


            }

            //public void llamar_phonereposity_save_si_la_no_existe_y_esta_llena()
            //{
            //    //Arrange
            //    var userProfileController = new UserProfileController();

            //    var userphonerepoMock = new Mock<IWPRepository<UserPhone>>();
            //    var useraddressrepoMock = new Mock<IWPRepository<UserAddress>>();
            //    var userprofilerepoMock = new Mock<IWPRepository<UserProfile>>();

            //    userProfileController.userphonerepository = userphonerepoMock.Object;
            //    userProfileController.useraddressrepository = useraddressrepoMock.Object;
            //    userProfileController.userprofilerepository = userprofilerepoMock.Object;

            //    var userProfile = new UserProfile
            //    {
            //        UserPhone = new UserPhone
            //        {
            //            UserId = 123,
            //            Number = 12345678
            //        }
            //    };

            //    //Act
            //    userProfileController.Put(userProfile);
                


            //    //Assert
            //    userphonerepoMock.Verify(x => x.Save(It.IsAny<UserPhone>()));
                
            //}
        
        }
    }
}

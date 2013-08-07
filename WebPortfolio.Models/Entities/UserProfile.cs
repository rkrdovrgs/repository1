//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPortfolio.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.UserAddresses = new HashSet<UserAddress>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }       
        public string UserEmail { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
    
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}

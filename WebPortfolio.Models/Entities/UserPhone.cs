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
    
    public partial class UserPhone
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Number { get; set; }
    
        public virtual UserProfile UserProfile { internal get; set; }
    }
}

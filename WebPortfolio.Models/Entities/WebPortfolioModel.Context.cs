﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebPortfolioEntities : DbContext
    {
        public WebPortfolioEntities()
            : base("name=WebPortfolioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}

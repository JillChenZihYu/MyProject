﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ReserveRobotNewNewEntities : DbContext
    {
        public ReserveRobotNewNewEntities()
            : base("name=Home") //ReserveRobotNewNewEntities //Home
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administers> Administers { get; set; }
        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<FavoriteLists> FavoriteLists { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Reserves> Reserves { get; set; }
        public virtual DbSet<Restaurants> Restaurants { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}

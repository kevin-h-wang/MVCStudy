﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class qds162435604_dbEntities : DbContext
    {
        public qds162435604_dbEntities()
            : base("name=qds162435604_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblCarNO> tblCarNO { get; set; }
        public DbSet<tblCarNO_bak> tblCarNO_bak { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}

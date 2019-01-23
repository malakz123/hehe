namespace hehe.DB
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using hehe.Models.DBModels;

    public class heheDB : DbContext
    {
        // Your context has been configured to use a 'heheDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'hehe.DB.heheDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'heheDB' 
        // connection string in the application configuration file.
        public heheDB()
            : base("name=heheDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
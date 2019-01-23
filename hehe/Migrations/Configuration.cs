using hehe.DB;
using hehe.Models.DBModels;

namespace hehe.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<hehe.DB.heheDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(hehe.DB.heheDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ProductCategories.AddOrUpdate(p => p.ProductCategoryId,
                new ProductCategory()
                {
                    Name = "Dairy",
                    ProductCategoryId = 1
                },
                new ProductCategory()
                {
                    Name = "Pasta",
                    ProductCategoryId = 2
                },
                new ProductCategory()
                {
                    Name = "Bread",
                    ProductCategoryId = 3
                });

            context.Products.AddOrUpdate(p => p.ProductId,
                new Product
                {
                    ProductId = 1,
                    ProductCategoryId = 1,
                    Name = "Milk",
                    Description = "Some lovely milk.",
                    Price = 20
                },
                new Product
                {
                    ProductId = 2,
                    ProductCategoryId = 1,
                    Name = "Yoghurt",
                    Description = "Some lovely Yoghurt.",
                    Price = 25
                });

            context.Products.AddOrUpdate(p => p.ProductId,
                new Product
                {
                    ProductId = 3,
                    ProductCategoryId = 2,
                    Name = "Makaroner",
                    Description = "Some lovely Makaroner.",
                    Price = 15
                },
                new Product
                {
                    ProductId = 4,
                    ProductCategoryId = 2,
                    Name = "Spiral Pasta",
                    Description = "Some lovely not Makaroner.",
                    Price = 25
                });
            context.Products.AddOrUpdate(p => p.ProductId,
                new Product
                {
                    ProductId = 5,
                    ProductCategoryId = 3,
                    Name = "Bread",
                    Description = "Some lovely bread.",
                    Price = 17
                });
        }
    }
}
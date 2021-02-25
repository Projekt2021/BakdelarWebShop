using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DataAccess
{
    public class SeedData
    {
        public SeedData()
        {

        }

        public static void Seeding(BakdelarAppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                     new Category
                     {
                         CategoryName = "Redskap"
                     },

                     new Category
                     {
                         CategoryName = "Köksmaskiner"
                     },

                     new Category
                     {
                         CategoryName = "Ingredienser"
                     });

                context.SaveChanges();
            }
        }
        //public IList<Category> Category { get; set; }

        public static void SeedingProducts(BakdelarAppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                     new Product
                     {
                         ProductName = "Stavmixer",
                         ProductDescription = "Stavmixer",
                         ProductPrice = 30,
                         AvailableQuantity = 10,
                         DateEntered = new DateTime(2021, 02, 23),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmakiner.Stavmixer.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Baktermometer.png" }}

                     });



                context.SaveChanges();
            }
        }
    }
}

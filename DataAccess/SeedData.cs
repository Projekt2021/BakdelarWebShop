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

        //context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault()
        //new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver.png" },
        //new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver2.jpg" }

        public static void SeedingProducts(BakdelarAppDbContext context)
        {
            if (!context.Products.Any())
            {

                                          new Product("Våg", "Vågarnas vågade våg", 210, 3, 325, null, false, new DateTime(2021, 01, 11),
                                          context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Våg.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Våg2.jpg" } }),

                                          new Product("Bunke", "Bunke av bunkigast slag", 40, 16, 33, null, false, new DateTime(2021, 01, 03),
                                          context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Bunke.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Redskap.Bunke2.jpg" } }),

                                          new Product("Kavel", "Plattar effektivt till deg", 55, 5, 110, 19, true, new DateTime(2021, 02, 26),
                                          context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Kavel.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\cow.jpg" } }),

                                          new Product("Mått", "Måtte de portionera", 25, 18, 45, null, false, new DateTime(2021, 02, 28),
                                          context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Mått.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Redskap.Mått2.jpg" } }),

                                          new Product("Siktare", "Homogeniserar pulver", 48, 2, 50, null, false, new DateTime(2021, 02, 02),
                                          context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Siktare.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Redskap.Siktare2.jpg" } }),

                                          new Product("Bakpulver", "Bakigt pulver för fluff av verk", 38, 16, 45, 21, false, new DateTime(2021, 02, 02),
                                          context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver2.jpg" } }),

                                          new Product("Bunke-Mått-Set", "Set af Bunk und Motte", 48, 4, 60, null, false, new DateTime(2021, 02, 11),
                                          context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                                          new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.BunkeMåttSet.png" },
                                      new ProductImage { ImageURL = "\\images\\product\\Redskap.BunkeMåttSet2.jpg" } })
                                          );

                context.SaveChanges();
            }
        }
    }
}

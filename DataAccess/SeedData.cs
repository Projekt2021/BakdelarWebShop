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
                         ProductName = "Bakpulver",
                         ProductDescription = "Fluffar upp bakverk",
                         ProductPrice = 12,
                         AvailableQuantity = 10,
                         ProductWeight = 60,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 23),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Bakpulver2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Grön Karamellfärg",
                         ProductDescription = "Färgar bakverk gröna",
                         ProductPrice = 30,
                         AvailableQuantity = 22,
                         ProductWeight = 25,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 03),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.GrönKaramellfärg.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Ingredienser.GrönKaramellfärg2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Mjöl",
                         ProductDescription = "Blir till bakverk",
                         ProductPrice = 32,
                         AvailableQuantity = 3,
                         ProductWeight = 500,
                         SpecialPrice = 23,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 01, 23),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Mjöl.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Mjöl2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Strössel",
                         ProductDescription = "Kan vara skoj",
                         ProductPrice = 15,
                         AvailableQuantity = 22,
                         ProductWeight = 12,
                         SpecialPrice = 22,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 05),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Strössel.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Strössel2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Vaniljstång",
                         ProductDescription = "Torkad stång av vaniljfrukt",
                         ProductPrice = 38,
                         AvailableQuantity = 8,
                         ProductWeight = 4,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 01, 25),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Ingredienser").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Vaniljstång.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Ingredienser.Vaniljstång2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Stavmixer",
                         ProductDescription = "Mixer i stavupplägg",
                         ProductPrice = 289,
                         AvailableQuantity = 3,
                         ProductWeight = 50,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 09),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Stavmixer.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Stavmixer2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Baktermometer",
                         ProductDescription = "Termometer för bröd",
                         ProductPrice = 99,
                         AvailableQuantity = 7,
                         ProductWeight = 25,
                         SpecialPrice = null,
                         IsSelected = true,
                         DateEntered = new DateTime(2021, 02, 25),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Baktermometer.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Baktermometer2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Köksassistent",
                         ProductDescription = "Knådar deg och vispar",
                         ProductPrice = 329,
                         AvailableQuantity = 2,
                         ProductWeight = 700,
                         SpecialPrice = 299,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 04),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Köksassistent.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Köksassistent2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Mixer",
                         ProductDescription = "Storartad mixer",
                         ProductPrice = 289,
                         AvailableQuantity = 6,
                         ProductWeight = 285,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 18),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Mixer.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Mixer2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Våg",
                         ProductDescription = "Havets vågade våg",
                         ProductPrice = 319,
                         AvailableQuantity = 3,
                         ProductWeight = 480,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 14),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Köksmaskiner").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Våg.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Köksmaskiner.Våg2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Bunke",
                         ProductDescription = "Bunke av plastigaste slag",
                         ProductPrice = 35,
                         AvailableQuantity = 21,
                         ProductWeight = 26,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 01, 29),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Bunke.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Redskap.Bunke2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Bunke-Mått-Set",
                         ProductDescription = "Bunkar och måttare",
                         ProductPrice = 78,
                         AvailableQuantity = 8,
                         ProductWeight = 205,
                         SpecialPrice = 68,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 13),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.BunkeMåttSet.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Redskap.BunkeMåttSet2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Kavel",
                         ProductDescription = "Plattar effektivt till deg",
                         ProductPrice = 65,
                         AvailableQuantity = 12,
                         ProductWeight = 360,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 13),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Kavel.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Redskap.Kavel2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Mått",
                         ProductDescription = "Måtte de portionera",
                         ProductPrice = 39,
                         AvailableQuantity = 17,
                         ProductWeight = 38,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 01, 04),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Mått.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Redskap.Mått2.jpg" }}

                     },
                     new Product
                     {
                         ProductName = "Siktare",
                         ProductDescription = "Homogeniserar pulver",
                         ProductPrice = 35,
                         AvailableQuantity = 3,
                         ProductWeight = 45,
                         SpecialPrice = null,
                         IsSelected = false,
                         DateEntered = new DateTime(2021, 02, 13),
                         CategoryId = context.Categories.Where(x => x.CategoryName == "Redskap").Select(x => x.CategoryId).FirstOrDefault(),
                         ProductImages = new List<ProductImage> { new ProductImage { ImageURL = "\\images\\product\\Redskap.Siktare.png" },
                         new ProductImage { ImageURL = "\\images\\product\\Redskap.Siktare2.jpg" }}

                     });



                context.SaveChanges();
            }
        }
    }
}

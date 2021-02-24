using DataAccess.DataModels;
using System;
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
    }
}

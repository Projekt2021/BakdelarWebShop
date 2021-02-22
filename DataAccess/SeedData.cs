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
                         CategoryName = "Födelsedag"
                     },

                     new Category
                     {
                         CategoryName = "Valentine"
                     },

                     new Category
                     {
                         CategoryName = "Anniversary"
                     },

                     new Category
                     {
                         CategoryName = "Bakverktyg"
                     },

                     new Category
                     {
                         CategoryName = "Apparater"
                     });
                context.SaveChanges();
            }
        }
    }
}

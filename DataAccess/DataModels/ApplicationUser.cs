using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}

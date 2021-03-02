using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.ViewModels
{
    public class CategoryView
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}

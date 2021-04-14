using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class CategoryView
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string CategoryName { get; set; }
        //public List<SelectListItem> Categories { set; get; }
    }
}

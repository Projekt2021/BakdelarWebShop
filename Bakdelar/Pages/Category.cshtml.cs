using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakdelar.Pages
{
    public class CategoryModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public void OnGet()
        {
        }
    }
}

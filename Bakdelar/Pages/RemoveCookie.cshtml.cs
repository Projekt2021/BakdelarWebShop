using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakdelar.Pages
{
    public class RemoveCookieModel : PageModel
    {
        public void OnGet()
        {

            Response.Cookies.Delete("shopping_basket");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Session;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bakdelar.Classes;
using System.Text;

namespace Bakdelar.Pages
{
    public class ProductModel : PageModel
    {
        IConfiguration _configuration { get; set; }



        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }


        public ProductView Product { get; set; }

        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task OnGetAsync()
        {
        }
        
    }
}

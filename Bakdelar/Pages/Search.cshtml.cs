using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.MethodClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.Pages
{
    public class SearchModel : PageModel
    {


        private readonly IConfiguration _configuration;


        public SearchModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNo { get; set; }


        public List<Classes.ProductView> Products { get; set; }

        public async Task OnGetAsync()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                using var httpClient = new HttpClient();
                Products = await GetFromApi.SearchProducts(Name);
                //Products = Products.Skip(12 * PageNo - 1)
                //                   .Take(12)
                //                   .ToList();

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.Pages
{
    public class CategoryModel : PageModel
    {
        IConfiguration _configuration { get; set; }

        public CategoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }




        public List<ProductView> Products { get; set; }
        public async Task OnGetAsync()
        {
            if(!string.IsNullOrWhiteSpace(Id))
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    using var httpClient = new HttpClient();
                    Products = await httpClient.GetFromJsonAsync<List<Classes.ProductView>>($"{_configuration.GetValue<String>("APIEndpoint")}api/Category/Search?Id={Id}");
                    //Products = Products.Skip(12 * PageNo - 1)
                    //                   .Take(12)
                    //                   .ToList();

                }
            }
        }
    }
}

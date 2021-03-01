using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.Pages
{
    public class ProductModel : PageModel
    {
        IConfiguration _configuration { get; set; }

        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            using var httpClient = new HttpClient();
            //httpClient.GetFromJsonAsync<Classes.ProductView>($"{ _configuration.GetValue<String>("APIEndpoint")}"
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bakdelar.Pages.Shared
{
    public class SingleProductViewModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public SingleProductViewModel(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public ProductView Product { get; set; }

        public bool Error = false;


        public async Task<IActionResult> OnGet(int? id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Product = await httpClient.GetFromJsonAsync<ProductView>($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{id.Value}");
                }
                catch (Exception)
                {
                    if (Product.ProductName == null)
                    {
                        Error = true;
                    }
                }
            }

            return Page();

        }
    }
}

using Bakdelar.Classes;
using Bakdelar.MethodClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bakdelar.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public IndexModel(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        //[BindProperty]
        public List<ProductView> ProductsOnSale { get; set; }
        public List<ProductView> ProductsSelected { get; set; }
        public List<ProductView> ProductsNew { get; set; }
        public List<ProductView> ProductsMostSold { get; set; }

        /// <summary>  
        /// GET: /Index  
        /// </summary>  
        /// <returns>Returns - Appropriate page </returns>  
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ProductsOnSale = await GetFromApi.GetAllProductsAsync("/Sale/4");
            ProductsMostSold = await GetFromApi.GetAllProductsAsync("/MostSold/4");
            ProductsSelected = await GetFromApi.GetAllProductsAsync("/Selected/4");
            ProductsNew = await GetFromApi.GetAllProductsAsync("/Newest/4");

            return Page();
        }
    }
}

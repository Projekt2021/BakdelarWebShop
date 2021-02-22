using Bakdelar.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bakdelar.Pages.Admin.Product
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _hostingEnvironment;


        public CreateModel(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }


        [BindProperty]
        public ProductView Product { get; set; }
        public List<CategoryView> Categories { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await GetCategory();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.ProductImageView = new List<ProductImageView>();
            string wwwPath = this._hostingEnvironment.WebRootPath;
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, _configuration.GetValue<String>("ProducImagePath"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (IFormFile postedFile in files)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    Product.ProductImageView.Add(new ProductImageView
                    {
                        ImageURL = $"\\{_configuration.GetValue<String>("ProducImagePath")}{ fileName}"
                    });
                }
            }

            var token = HttpContext.Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token))
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{_configuration.GetValue<String>("APIEndpoint")}api/product", Product);

        if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task GetCategory()
        {
            using HttpClient httpClient = new HttpClient();
            var user = await _userManager.GetUserAsync(User);
            var token = HttpContext.Request.Cookies["access_token"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Categories = await httpClient.GetFromJsonAsync<List<CategoryView>>($"{_configuration.GetValue<String>("APIEndpoint")}api/category");
        }




    }
}

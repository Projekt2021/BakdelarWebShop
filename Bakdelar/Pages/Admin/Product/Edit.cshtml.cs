using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bakdelar.Areas.Identity.Data;
using Bakdelar.Classes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bakdelar.Pages.Admin.Product
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnviroment;
        


        public EditModel(
           IConfiguration configuration,
           UserManager<MyUser> userManager,
           ILogger<IndexModel> logger,
           IHostingEnvironment environment)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _hostingEnviroment = environment;
        }

        [BindProperty]
        public ProductView ProductView { get; set; }
        public List<CategoryView> Categories { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {

            var token = HttpContext.Request.Cookies["access_token"];

            if (string.IsNullOrEmpty(token))
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ProductView = await client.GetFromJsonAsync<ProductView>($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{id.Value}");
            Categories = await client.GetFromJsonAsync<List<CategoryView>>($"{_configuration.GetValue<String>("APIEndpoint")}api/category");

            if (ProductView == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<IFormFile> files, int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductView.ProductImageView = new List<ProductImageView>();
            string wwwPath = this._hostingEnviroment.WebRootPath;
            string path = Path.Combine(this._hostingEnviroment.WebRootPath, _configuration.GetValue<String>("ProducImagePath"));
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
                    ProductView.ProductImageView.Add(new ProductImageView
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

            HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                    $"{_configuration.GetValue<string>("APIEndpoint")}api/product/{ProductView.ProductId}", ProductView);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteImageAsync(int? id)
        {

            using HttpClient httpClient = new HttpClient();
            var token = HttpContext.Request.Cookies["access_token"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.DeleteAsync($"{_configuration.GetValue<string>("APIEndpoint")}api/product/DeleteProductImage/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                int productId = int.Parse(response.Content.ReadAsStringAsync().Result);
                return RedirectToPage("./Edit", new { id = productId });
            }
            return Page();
        }
    }
}

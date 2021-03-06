using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Bakdelar.MethodClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public string CategoryName { get; set; }
        public List<ProductView> Products { get; set; }
        public async Task OnGetAsync()
        {

            if (!string.IsNullOrWhiteSpace(Id))
            {
                using var httpClient = new HttpClient();
                Products = await httpClient.GetFromJsonAsync<List<Classes.ProductView>>($"{_configuration.GetValue<string>("APIEndpoint")}api/Category/Search?Id={Id}");

                //        //Products = Products.Skip(12 * PageNo - 1)
                //        //                   .Take(12)
                //        //                   .ToList();


                await SetBreadcrumb();
            }
            else if (!string.IsNullOrWhiteSpace(Filter))
            {
                string filter = Filter.ToLower();
                if (filter == "newest" || filter == "selected" ||
                    filter == "mostsold" || filter == "sale")
                {
                    Products = await GetFromApi.GetAllProductsAsync($"/{filter}/0");
                }
            }


        }
        private async Task SetBreadcrumb()
        {
            using var httpClient = new HttpClient();
            var categoryView = await httpClient.GetFromJsonAsync<CategoryView>($"{_configuration.GetValue<string>("APIEndpoint")}api/Category/{Id}");

            if (categoryView != null)
            {
                CategoryName = categoryView.CategoryName;
                ViewData["breadcrumb"] = new BreadcrumbsView() { Category = categoryView };
            }
            //else if()
        }


    }
}

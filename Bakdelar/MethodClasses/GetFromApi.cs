using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.MethodClasses
{
    public class GetFromApi
    {


        private static HttpClient httpClient = new();

        private static string products = "";
        private static string categories = "";
        private static string productImages = "";

        public static string ProductLink { get
            {
                return products;
            }
            private set
            {
                products = value;
            }
        }

        public static string CategoryLink
        {
            get
            {
                return categories;
            }
            private set
            {
                categories = value;
            }
        }


        public static string ProductImagesLink
        {
            get
            {
                return productImages;
            }
            private set
            {
                productImages = value;
            }
        }



        public static void SetupLinks(IConfiguration configuration)
        {
            ProductLink = $"{configuration.GetValue<String>("APIEndpoint")}api/product";
            CategoryLink = $"{configuration.GetValue<String>("APIEndpoint")}api/category";
            ProductImagesLink = $"{configuration.GetValue<String>("APIEndpoint")}api/ProductImage";
        }



        public static async Task<List<ProductView>> GetAllProductsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<ProductView>>(ProductLink);
        }


        public static async Task<List<ProductView>> GetAllProductsAsync(string function)
        {
            string a = ProductLink + $"/{function}";
            return await httpClient.GetFromJsonAsync<List<ProductView>>(ProductLink + $"{function}");
        }


        public static async Task<List<ProductView>> SearchProducts(string term)
        {

            return await httpClient.GetFromJsonAsync<List<ProductView>>(ProductLink + $"/Search?Name={term}");
        }
        
        
        public static async Task<List<ProductView>> GetProductsInCategory(int id)
        {
            return await httpClient.GetFromJsonAsync<List<ProductView>>(CategoryLink + $"/Search?Id={id}");
        }



        public static async Task<List<CategoryView>> GetAllCategoriesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<CategoryView>>(CategoryLink);
        }



        public static async Task<CategoryView> GetCategoryAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<CategoryView>(CategoryLink + $"/{id}");
        }


        public static async Task<ProductView> GetProductAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<ProductView>(ProductLink + $"/{id}");
        }

        public static async Task<ProductView> GetProductAsync(string function)
        {
            return await httpClient.GetFromJsonAsync<ProductView>(ProductLink + $"/{function}");
        }

        /// <summary>
        /// PRODUCTIMAGES BLIR NULL!!!!
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutProductAsync(ProductView product)
        {
            int id = product.ProductId;
            product.ProductImageView = null;
            return await httpClient.PutAsJsonAsync(ProductLink + $"/{id}", product);
        }

        public static async Task<HttpResponseMessage> PutCategoryAsync(CategoryView category)
        {
            int id = category.CategoryId;
            return await httpClient.PutAsJsonAsync(ProductLink + $"/{id}", category);
        }

    }
}

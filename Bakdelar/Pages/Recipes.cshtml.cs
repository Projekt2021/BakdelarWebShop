using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherAPI.Models.External;

namespace Bakdelar.Pages
{
    public class RecipesModel : PageModel
    {
        // public List<Hit> Recipes { get; set; }

        public EdamamRecipe Recipe { get; set; }

        [BindProperty]
        [Display(Name = "Ingrediens")]
        public string Ingredient { get; set; }



        public async Task OnGetAsync()
        {
            //var client = new HttpClient();

            //Task<string> getRecipeStringTask = client.GetStringAsync($"https://api.edamam.com/search?q=apple&app_id=58e02eb1&app_key=75c99118aeec150db8dabec724cb270f&mealType=snack&from=0&to=1");
            //string recipesString = await getRecipeStringTask;

            ////Recipes = JsonSerializer.Deserialize<List<Hit>>(recipesString).ToList();

            //Recipe = JsonSerializer.Deserialize<EdamamRecipe>(recipesString);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();


            Task<string> getRecipeStringTask =
                client.GetStringAsync($"https://api.edamam.com/search?q={Ingredient}&app_id=58e02eb1&app_key=75c99118aeec150db8dabec724cb270f&mealType=snack&from=0&to=1");

            string recipeString = await getRecipeStringTask;

            try
            {
                Recipe = JsonSerializer.Deserialize<EdamamRecipe>(recipeString);
            }
            catch (Exception)
            {
                return Page(); ;
            }

            return Page();
        }
    }
}

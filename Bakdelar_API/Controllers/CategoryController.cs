using Bakdelar_API.ViewModels;
using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.Controllers
{
    //[Authorize("AdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BakdelarAppDbContext _context;

        public CategoryController(BakdelarAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }



        // GET: api/Categories/5
        [HttpGet("Search")]
        public async Task<List<ProductView>> GetProductsInCategory(int id)
        {
            if(!_context.Categories.Any(category => category.CategoryId == id))
            {
                return new List<ProductView>();
            }
            var result = await _context.Products
                                       .Where(product => product.Category.CategoryId == id)
                                       .Include(prod => prod.ProductImages)
                                       .Select(p => new ProductView
                                       {
                                           ProductId = p.ProductId,
                                           ProductName = p.ProductName,
                                           ProductDescription = p.ProductDescription,
                                           ProductImageView = p.ProductImages
                                                               .Select(pi => new ProductImageView
                                                               {
                                                                   ImageId = pi.ImageId,
                                                                   ImageURL = pi.ImageURL
                                                               }).ToList(),
                                           ProductPrice = p.ProductPrice,
                                           CategoryId = p.CategoryId,
                                           ProductWeight = p.ProductWeight,
                                           AvailableQuantity = p.AvailableQuantity
                                       })
                                       .ToListAsync();

            return result;
        }





        // GET: api/Categories/5
        [HttpGet("{id}")]        
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}

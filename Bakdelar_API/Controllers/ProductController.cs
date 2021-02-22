using Bakdelar_API.ViewModels;
using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly BakdelarAppDbContext _context;

        public ProductController(BakdelarAppDbContext context)
        {
            _context = context;
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        //// GET: api/Products
        [HttpGet]
        public async Task<object> GetAllProductsAsync()
        {
            var productList = _context.Products.Select(p => new ProductView
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                AvailableQuantity = p.AvailableQuantity,
                ProductWeight = p.ProductWeight,
                //Cascade insert
                Category = new CategoryView
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName
                },
                ProductImageView = p.ProductImages.Select(x => new ProductImageView { ImageId = x.ImageId, ImageURL = x.ImageURL }).ToList()
            }).ToList();

            return productList;
        }

        // GET: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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



        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductView productView)
        {
            Product product = new Product
            {
                AvailableQuantity = productView.AvailableQuantity,
                CategoryId = productView.CategoryId,
                ProductDescription = productView.ProductDescription,
                ProductName = productView.ProductName,
                ProductPrice = productView.ProductPrice,
                ProductWeight = productView.ProductWeight,
                ProductImages = productView.ProductImageView.Select(x => new ProductImage
                {
                    ImageURL = x.ImageURL
                }).ToList()
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

    }
}

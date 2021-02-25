using Bakdelar_API.ViewModels;
using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<ActionResult<ProductView>> GetProducts(int id)
        {
            var productView = await _context.Products.Where(x => x.ProductId == id).Select(product =>
                          new ProductView
                          {
                              ProductId = product.ProductId,
                              ProductName = product.ProductName,
                              ProductDescription = product.ProductDescription,
                              ProductPrice = product.ProductPrice,
                              SpecialPrice = product.SpecialPrice,
                              AvailableQuantity = product.AvailableQuantity,
                              ProductWeight = product.ProductWeight,
                              DateEntered = product.DateEntered,
                              IsSelected = product.IsSelected,
                              //NumberOfSold = product.NumberOfSold,                         
                              CategoryId = product.CategoryId,
                              Category = new CategoryView
                              {
                                  CategoryId = product.Category.CategoryId,
                                  CategoryName = product.Category.CategoryName
                              },
                              ProductImageView = product.ProductImages.Select(x => new ProductImageView { ImageId = x.ImageId, ImageURL = x.ImageURL }).ToList()
                          }).FirstOrDefaultAsync();

            // Get product from database
            if (productView == null)
            {
                return NotFound();
            }
            return productView;
        }

        //// GET: api/Products
        [HttpGet]
        public async Task<List<ProductView>> GetAllProductsAsync()
        {
            var productList = await _context.Products.Select(p => new ProductView
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                SpecialPrice = p.SpecialPrice,
                AvailableQuantity = p.AvailableQuantity,
                ProductWeight = p.ProductWeight,
                DateEntered = p.DateEntered,
                IsSelected = p.IsSelected,               
                //Cascade insert
                Category = new CategoryView
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName
                },
                ProductImageView = p.ProductImages.Select(x => new ProductImageView { ImageId = x.ImageId, ImageURL = x.ImageURL }).ToList()
            }).ToListAsync();

            return productList;
        }

        // GET: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, ProductView product)
        {
            Product productDB = await _context.Products.FindAsync(id);

            if (productDB == null)
            {
                return NotFound();
            }
            else
            {
                productDB.ProductName = product.ProductName;
                productDB.ProductDescription = product.ProductDescription ?? productDB.ProductDescription;
                productDB.ProductPrice = product.ProductPrice;
                productDB.SpecialPrice = product.SpecialPrice;
                productDB.AvailableQuantity = product.AvailableQuantity;
                productDB.ProductWeight = product.ProductWeight;
                productDB.CategoryId = product.CategoryId;
                //productDB.DateEntered = product.DateEntered;  //DateEntered should not be changed
                productDB.IsSelected = product.IsSelected;               
            }

            _context.Entry(productDB).State = EntityState.Modified;

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


        //public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}



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
                SpecialPrice = productView.SpecialPrice,
                ProductWeight = productView.ProductWeight,
                DateEntered = DateTime.Now,
                IsSelected = productView.IsSelected,
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

    }
}

using Bakdelar_API.ViewModels;
using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.Controllers
{
    [AllowAnonymous]
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
                              NumberOfSold = product.NumberOfSold,
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
        [HttpGet("Search")]
        public async Task<object> SearchProducts(string Name)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = Name.ToLower();
                string[] searchQuery = Name.Split(" ");


                List<ProductView> result = new();
                foreach (var word in searchQuery)
                {
                    List<ProductView> productViews = _context.Products.Where(product => product.ProductName.Contains(word) || product.ProductDescription.Contains(word))
                                                     .Include(product => product.ProductImages)
                                                     .Select(product => new ProductView
                                                     {
                                                         ProductId = product.ProductId,
                                                         ProductName = product.ProductName,
                                                         ProductDescription = product.ProductDescription,
                                                         ProductPrice = product.ProductPrice,
                                                         AvailableQuantity = product.AvailableQuantity,
                                                         ProductWeight = product.ProductWeight,
                                                         SpecialPrice = product.SpecialPrice,
                                                         NumberOfSold = product.NumberOfSold,
                                                         //Cascade insert
                                                         Category = new CategoryView
                                                         {
                                                             CategoryId = product.Category.CategoryId,
                                                             CategoryName = product.Category.CategoryName
                                                         },
                                                         ProductImageView = product.ProductImages.Select(x => new ProductImageView { ImageId = x.ImageId, ImageURL = x.ImageURL }).ToList()
                                                     }).ToList();
                    foreach (var product in productViews)
                    {
                        bool alreadyInList = result.Any(prod => prod.ProductId == product.ProductId);
                        if (alreadyInList)
                        {
                            continue;
                        }
                        else
                        {
                            result.Add(product);
                        }
                    }
                }
                return result;
            }
            else
            {
                return new List<ProductView>();
            }


        }

        //// GET: api/Products
        [HttpGet]
        public async Task<List<ProductView>> GetAllProductsAsync()
        {
            var productList = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).Select(p => new ProductView(p)).ToListAsync();

            return productList;
        }

        // GET: api/Products/5
        [Authorize(Policy = "RequireAdministratorRole")]
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

                productDB.NumberOfSold = product.NumberOfSold;
                //productDB.DateEntered = product.DateEntered;  //DateEntered should not be changed
                productDB.IsSelected = product.IsSelected;
                productDB.ProductImages = product.ProductImageView.Select(x => new ProductImage
                {
                    ImageURL = x.ImageURL,
                    ProductId = product.ProductId
                }).ToList();
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

        [Authorize(Policy = "RequireAdministratorRole")]
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


        [Authorize(Policy = "RequireAdministratorRole")]
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


        [Authorize(Policy = "RequireAdministratorRole")]
        // DELETE: api/TodoItems/5
        [HttpDelete("DeleteProductImage/{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            int prodId = productImage.ProductId;

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();

            return Ok(prodId);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

    }
}

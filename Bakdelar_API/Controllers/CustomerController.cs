using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Bakdelar_API.ViewModels;

namespace Bakdelar_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BakdelarAppDbContext _context;

        public CustomerController(BakdelarAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerView>> Get(string id)
        {
            var customer = await _context.Customer.Where(x => x.UserId == Guid.Parse(id)).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return new CustomerView
            {
                Address = customer.UserAddress,
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                UserId = customer.UserId
            };
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> AddCustomer(string id)
        {
            if (!CustomerExists(id))
            {
                _context.Customer.Add(new Customer { UserId = Guid.Parse(id) });
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, CustomerView customerView)
        {
            var customer = _context.Customer.Where(x => x.UserId == Guid.Parse(id)).FirstOrDefault();
            if (customer == null)
            {
                customer = new Customer
                {
                    FirstName = customerView.FirstName,
                    LastName = customerView.LastName,
                    UserAddress = customerView.Address,
                    PhoneNumber = customerView.PhoneNumber,
                    UserId = Guid.Parse(id)
                };
                _context.Entry(customer).State = EntityState.Added;
            }
            else
            {
                customer.FirstName = customerView.FirstName;
                customer.LastName = customerView.LastName;
                customer.UserAddress = customerView.Address;
                customer.PhoneNumber = customerView.PhoneNumber;
                _context.Entry(customer).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.UserId == Guid.Parse(id));
        }
    }
}

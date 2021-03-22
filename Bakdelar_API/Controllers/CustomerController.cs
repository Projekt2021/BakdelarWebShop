using DataAccess;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost("{id}")]
        public async Task<IActionResult> AddCustomer(string id)
        {
            if (!_context.Customer.Any(x => x.UserId == Guid.Parse(id)))
            {
                _context.Customer.Add(new Customer { UserId = Guid.Parse(id) });
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

    }
}

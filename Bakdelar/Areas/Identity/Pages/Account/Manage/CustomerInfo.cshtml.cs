using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bakdelar.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class UpdateAddressModel : PageModel
    {
        public UpdateAddressModel(UserManager<MyUser> userManager, AuthenticationDbContext authenticationDbContext, 
                                  ILogger<UpdateAddressModel> logger, SignInManager<MyUser> signInManager)
        {
            _userManager = userManager;
            _authContext = authenticationDbContext;
            _signInManager = signInManager;
            _logger = logger;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Address Address { get; set; }


        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }


        public UserManager<MyUser> _userManager { get; }
        public SignInManager<MyUser> _signInManager { get; }
        public ILogger<UpdateAddressModel> _logger { get; }
        public AuthenticationDbContext _authContext { get; }

        public void OnGet()
        {
            string userID = _userManager.GetUserId(User);
            var user = _authContext.MyUsers.Where(user => user.Id == userID).Include(user => user.Address).FirstOrDefault();
            Address = user.Address;
            FirstName = user.FirstName;
            LastName = user.LastName;
            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userID = _userManager.GetUserId(User);
            var user = _authContext.MyUsers.Where(user => user.Id == userID).Include(user => user.Address).FirstOrDefault();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.FirstName = FirstName;
            user.LastName = LastName;
            if(user.Address == null)
            {
                user.Address = Address;
            }
            else
            {
            user.Address.City = Address.City;
            user.Address.ZipCode = Address.ZipCode;
            user.Address.Street = Address.Street;
            }
            await _authContext.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their info successfully.");
            StatusMessage = "Informationen har uppdaterats.";

            return RedirectToPage();
        }



    }
}

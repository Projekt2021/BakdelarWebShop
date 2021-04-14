using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Bakdelar.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Bakdelar.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDbContext _authContext;

        public RegisterModel(
            UserManager<MyUser> userManager,
            SignInManager<MyUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IConfiguration configuration,
            AuthenticationDbContext authenticationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _configuration = configuration;
            _authContext = authenticationDbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }




        [BindProperty]
        [Required]
        [Display(Name = "Förnamn *")]
        public string Firstname { get; set; }




        [BindProperty]
        [Required]
        [Display(Name = "Efternamn *")]
        public string Lastname { get; set; }


        [BindProperty]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public Address Address { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email *")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Lösenord *")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekräfta lösenord *")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

        }

        public bool CreateRole()
        {
            bool x = _roleManager.RoleExistsAsync("Admin").ConfigureAwait(false).GetAwaiter().GetResult();
            if (!x)
            {
                // first we create Admin role 
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                _roleManager.CreateAsync(role).ConfigureAwait(false);               

                return true;
            }

            if (!_roleManager.RoleExistsAsync("Customer").ConfigureAwait(false).GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "Customer"
                };
                _roleManager.CreateAsync(role).ConfigureAwait(false);
            }
            return false;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Identity/Account/login");
            if (ModelState.IsValid)
            {
                bool isAdminUser = (CreateRole() || _userManager.Users.Count() == 0);
                var user = new MyUser { UserName = Input.Email, Email = Input.Email, FirstName = Firstname, LastName = Lastname, Address = Address, PhoneNumber = PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);//.ConfigureAwait(false).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    if (isAdminUser)
                        await _userManager.AddToRoleAsync(user, "Admin");
                    else
                        await _userManager.AddToRoleAsync(user, "Customer");

                    _logger.LogInformation("User created a new account with password.");

                    // dbcontext error - will fix this later
                    //_signInManager.SignInAsync(user, isPersistent: false).GetAwaiter().GetResult();
                    var loginUuser = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, lockoutOnFailure: false);
                    if (loginUuser.Succeeded)
                    {

                        user = _authContext.MyUsers.Where(user => user.Email == Input.Email).FirstOrDefault();
                        var roles = await _userManager.GetRolesAsync(user);
                        string token = GetToken(user, roles.FirstOrDefault());
                        HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions { HttpOnly = true, Secure = true });

                        _logger.LogInformation("User logged in.");

                        if (roles.Any(x => x == "Admin"))
                        { 
                            return LocalRedirect("/Admin/Product/");
                        }
                        else if(string.IsNullOrWhiteSpace(user.FirstName))
                        {
                            return LocalRedirect("/Identity/Account/Manage/CustomerInfo");
                        }
                        else
                        {
                            return LocalRedirect(returnUrl);
                        }
                    }
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        public String GetToken(MyUser user, string role)
        {
            var utcNow = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim (JwtRegisteredClaimNames.UniqueName,user.Email),
                    new Claim (JwtRegisteredClaimNames.Sub,user.Id.ToString()),		    
                    // Add the ClaimType Role which carries the Role of the user
                    new Claim (ClaimTypes.Role, role)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration.GetValue<String>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    notBefore: utcNow,
                    expires: utcNow.AddMinutes(this._configuration.GetValue<int>("Tokens:Lifetime")),
                    audience: this._configuration.GetValue<String>("Tokens:Audience"),
                    issuer: this._configuration.GetValue<String>("Tokens:Issuer")
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

}

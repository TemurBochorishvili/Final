using Final.Models;
using Final.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Final.Services;
using System.Security.Claims;

namespace Final.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _dbContext;

        private IJwtIssuer _jwtIssuer;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, IJwtIssuer jwtIssuer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _jwtIssuer = jwtIssuer;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Profile = new Profile
                    {
                        Email = model.Email,
                        TotalLimit = 0,
                        LimitLeft = 0,
                        PeriodTill = default(DateTime),
                        PeriodFrom = default(DateTime)
                    }
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Error");
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }

                result.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var profile = _dbContext.Profiles.Single(p => p.Id == user.ProfileId);

                    var jwt = _jwtIssuer.IssueJwt(new[] {
                        new Claim("username", user.UserName),
                        new Claim("email", user.Email),
                        new Claim("totalLimit", profile.TotalLimit.ToString())
                    });

                    var response = new
                    {
                        access_token = jwt,
                        username = model.Email
                    };

                    return Ok(response);
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", null);
        }

        [Authorize]
        public string Check()
        {
            return "Yes, You are loged in";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

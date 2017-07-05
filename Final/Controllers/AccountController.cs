using Final.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final.Controllers
{
    public class AccountController : Controller
    {
        //Dependency Injection with constructor
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password); //psw md5 შიფრაციაზე გადაყვანა შეიძლება დამჭირდეს
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false); // false -საიტის დახურვის შემდეგ აკეთებს ავტომატურ Log Out -ს
                    return RedirectToAction("Index", "Home"); // აბრუნებს Home კონტროლერის index - ზე    
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Loging Attemp...");
                }
            }

            return View();
        }

        [Autorize]
        public string Check()
        {
            return "Yes, You are loged in";
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

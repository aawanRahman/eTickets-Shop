using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext appDbContext;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
        }

        public IActionResult Login()
        {
           var logInVM = new LoginVM();
            return View(logInVM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);
            var user = await userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user!= null)
            {
                var passwordcheck = await userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordcheck)
                {
                   await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    return RedirectToAction("Index", "Movies");
                }

                TempData["Error"] = "Invalid Credentials. Please Try Again!";
                return View(loginVM);

            }
            TempData["Error"] = "Invalid Credentials. Please Try Again!";
            return View(loginVM);
        }
        public IActionResult Register()
        {
            var user = new RegisterVM();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await userManager.FindByEmailAsync(registerVM.EmailAddress);
            if( user!= null)
            {
                TempData["Error"] = " User Exists with this Email Address!";
                return View(registerVM);
            }
            
            var newUser = new ApplicationUser()
            {
                Email = registerVM.EmailAddress,
                FullName = registerVM.Name,
                UserName = registerVM.EmailAddress,
                EmailConfirmed = true
            };
            var response = await userManager.CreateAsync(newUser, registerVM.Password);
            if (response.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegistrationComplete");
            }
            return View(registerVM);
            


        }
        public IActionResult RegistrationComplete()
        {

            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");

        }
        public  IActionResult Users()
        {
            var allUserData =  userManager.Users.ToList();
            return View(allUserData);
        }
    }
}

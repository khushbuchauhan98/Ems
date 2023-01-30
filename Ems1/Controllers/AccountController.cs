using Data.Entities;
using Data.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ems1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Employees> _userManager;
        private readonly SignInManager<Employees> _signInManager;

        public AccountController(UserManager<Employees> userManager,
                              SignInManager<Employees> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Employees
                {
                    Firstname = model.Firstname,
                    Lastname=model.Lastname,
                    address= model.address,
                    DateofBirth= model.DateofBirth,
                    DateofJoin= model.DateofJoin,
                    AadharNumber= model.AadharNumber,
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedBy = _userManager.GetUserName(User),
                    CreatedDate= DateTime.Now,
                    EmailConfirmed=true,
                    
            };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                   // await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            //var branch = new Branch
            //{
            //    branchName = "Regie",
            //    address = "Naval"

            //};
            //branchContext.Branch.Add(branch);
            //branchContext.SaveChanges();

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}

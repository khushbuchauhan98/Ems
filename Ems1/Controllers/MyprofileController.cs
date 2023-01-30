using Data;
using Data.Entities;
using Data.FormModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ems1.Controllers
{
    public class MyprofileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDBContext _applicationDbContext;
        private readonly UserManager<Employees> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public MyprofileController(ILogger<HomeController> logger, AppDBContext applicationDbContext, UserManager<Employees> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Edit( )
        { 
            var uid = userManager.GetUserId(HttpContext.User);
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Employees user = userManager.FindByIdAsync(uid).Result;

                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,

                    };
                    
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            var uid = userManager.GetUserId(HttpContext.User);
            Employees user = userManager.FindByIdAsync(uid).Result;
            if (user == null)
            {
                ViewBag.err = $"User with Id={model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                
                    user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.address = model.address;
                user.DateofBirth = model.DateofBirth;
                user.DateofJoin = model.DateofJoin;
                user.AadharNumber = model.AadharNumber;

                user.UpdatedBy = userManager.GetUserName(User);
                user.UpdatedDate = DateTime.Now;
                    
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);

        }
        [HttpGet]
        public IActionResult Detail()
        {
            var uid = userManager.GetUserId(HttpContext.User);
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Employees user = userManager.FindByIdAsync(uid).Result;
                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task< IActionResult> Empindex()
        {
            List<RegisterViewModel> students = new List<RegisterViewModel>();
            var items = await userManager.GetUsersInRoleAsync("Employee");
            foreach (var i in items)
            {
                students.Add(new RegisterViewModel { Id=i.Id, Firstname = i.Firstname, Lastname = i.Lastname, address = i.address, AadharNumber = i.AadharNumber,DateofBirth=i.DateofBirth,DateofJoin=i.DateofJoin});
            }
            return View(students);  
        }
        [HttpGet]
        public async Task<IActionResult> Empedit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                //var uid = userManager.GetUserId(HttpContext.User);
                Employees user = await userManager.FindByIdAsync(id);


                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Empindex");
          

        }
        [HttpPost]
        public async Task<IActionResult> Empedit(RegisterViewModel employee)
        {
            var user = await userManager.FindByIdAsync(employee.Id);
            user.Firstname = employee.Firstname;
            user.Lastname = employee.Lastname;
            user.address = employee.address;
            user.DateofBirth = employee.DateofBirth;
            user.DateofJoin = employee.DateofJoin;
            user.AadharNumber = employee.AadharNumber;

            user.UpdatedBy = userManager.GetUserName(User);
            user.UpdatedDate = DateTime.Now;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Empindex");
            }

            return View(employee);      
        }
        [HttpGet]
        public async Task<IActionResult> Empdetail(string id)
        {         
            if (!string.IsNullOrEmpty(id))
            {
                //var uid = userManager.GetUserId(HttpContext.User);
                Employees user = await userManager.FindByIdAsync(id);


                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Empdelete(string id)
        {
            Employees user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Empindex");
                }
                else
                {
                    foreach (var er in result.Errors)
                        ModelState.AddModelError("", er.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public  IActionResult Allindex()
        {
            var a = (from user in _applicationDbContext.Employees
                     select new
                     {
                         Id= user.Id,
                         Firstname = user.Firstname,
                         Lastname = user.Lastname,
                         address = user.address,
                         AadharNumber = user.AadharNumber,
                         DateofBirth = user.DateofBirth,
                         DateofJoin = user.DateofJoin,

                     }).ToList().Select(p => new RegisterViewModel()
                     {
                       Id=p.Id,
                         Firstname = p.Firstname,
                         Lastname = p.Lastname,
                         address = p.address,
                         AadharNumber = p.AadharNumber,
                         DateofBirth = p.DateofBirth,
                         DateofJoin = p.DateofJoin,
                     });
            return View(a);
        }
        [HttpGet]
        public async Task<IActionResult> Alledit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                //var uid = userManager.GetUserId(HttpContext.User);
                Employees user = await userManager.FindByIdAsync(id);


                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Allindex");


        }
        [HttpPost]
        public async Task<IActionResult> Alledit(RegisterViewModel employee)
        {
            var user = await userManager.FindByIdAsync(employee.Id);
            user.Firstname = employee.Firstname;
            user.Lastname = employee.Lastname;
            user.address = employee.address;
            user.DateofBirth = employee.DateofBirth;
            user.DateofJoin = employee.DateofJoin;
            user.AadharNumber = employee.AadharNumber;

            user.UpdatedBy = userManager.GetUserName(User);
            user.UpdatedDate = DateTime.Now;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Allindex");
            }

            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Alldetail(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                //var uid = userManager.GetUserId(HttpContext.User);
                Employees user = await userManager.FindByIdAsync(id);


                if (user != null)
                {
                    RegisterViewModel model = new RegisterViewModel()
                    {
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        address = user.address,
                        DateofBirth = user.DateofBirth,
                        DateofJoin = user.DateofJoin,
                        AadharNumber = user.AadharNumber,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Alldelete(string id)
        {
            Employees user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Allindex");
                }
                else
                {
                    foreach (var er in result.Errors)
                        ModelState.AddModelError("", er.Description);
                }
            }
            return View();
        }
    }
}

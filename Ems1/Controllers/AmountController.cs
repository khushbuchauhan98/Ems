using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.Entities;
using Data.FormModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Ems1.Controllers
{
    public class AmountController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<Employees> _userManager;
        private AppDBContext _applicationDbContext;
        public AmountController(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IMapper mapper,
            UserManager<Employees> userManager,
            AppDBContext applicationDbContext

        )
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;

        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //ViewBag.cc = _applicationDbContext.Employees.Select(x => new SelectListItem { Text = x.Firstname, Value = x.Id }).ToList();
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            var list = users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }).ToList();
            ViewBag.Employee = list;
            //var leaveTypes = await _unitOfWork.Amount.FindAll();          
            return View();
        }

        // POST: Amount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AmountVM model)
        {

            try
            {

                var leaveTypes = await _unitOfWork.Amount.FindAll();

                var employee = await _userManager.GetUserAsync(User);
                var period = DateTime.Now.Year;
                var allocation = await _unitOfWork.LeaveAllocations.Find(q => q.EmployeeId == employee.Id
                                                    && q.Period == period
                                                   );
                var users = await _userManager.GetUsersInRoleAsync("Employee");
                SelectList list = new SelectList(users);
                ViewBag.Employee = list;

                var amountreqmodel = new AmountVM
                {
                    RequestingEmployeeId = model.RequestingEmployeeId,
                    SalaryAmount = model.SalaryAmount,
                    Salarydate = DateTime.Now,
                    CreatedbyId = employee.Id

                };

                var amountRequest = _mapper.Map<Amount>(amountreqmodel);

                amountRequest.CreatedBy = _userManager.GetUserName(User);
                await _unitOfWork.Amount.Create(amountRequest);
                await _unitOfWork.Save();

                // Send Email to supervisor and requesting user
                await _emailSender.SendEmailAsync("admin@localhost.com", "New Leave Request",
                    $"Please review this leave request. <a href='UrlOfApp/{amountRequest.Id}'>Click Here</a>");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult> EmpIndex()
        {
            var employee = await _userManager.GetUserAsync(User);
            var employeeid = employee.Id;      
            var employeeRequests = await _unitOfWork.Amount
                .FindAll(q => q.RequestingEmployeeId == employeeid);

           
            var employeeRequestsModel = _mapper.Map<List<AmountVM>>(employeeRequests);

            var model = new EmployeeDataVm
            {
                AmountVMs = employeeRequestsModel,
               
            };

            return View(model);
            //var employee = await _userManager.GetUserAsync(User);
            //var employeeid = employee.Id;
            //var getemployeeamount = _applicationDbContext.Amounts.Where(x => x.RequestingEmployeeId == employeeid).ToList();
            
            //return View(getemployeeamount);
        }
        [Authorize(Roles = "Superadmin")]
        public async Task<ActionResult> Index()
        {

            var leaveRequests = await _unitOfWork.Amount.FindAll(
                includes: q => q.Include(x => x.RequestingEmployee)
                .Include(x => x.Createdby)
            ); ;

            var leaveRequstsModel = _mapper.Map<List<AmountVM>>(leaveRequests);
            var model = new AmountRequestVM
            {
                Amounts = leaveRequstsModel
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var leavetype = await _unitOfWork.Amount.Find(expression: q => q.Id == id);
                if (leavetype == null)
                {
                    return NotFound();
                }
                _unitOfWork.Amount.Delete(leavetype);
                await _unitOfWork.Save();

            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

    }
}


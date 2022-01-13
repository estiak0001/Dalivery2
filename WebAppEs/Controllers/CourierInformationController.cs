using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.Models;
using WebAppEs.Services;
using WebAppEs.ViewModel.Courier;
using WebAppEs.ViewModel.SalesChannel;

namespace WebAppEs.Controllers
{
    public class CourierInformationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;

        public CourierInformationController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ISetupService setupService,
                ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _setupService = setupService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ListData = _setupService.GetAllCourierList();
            return View(ListData);
        }
        

        public IActionResult CreateCourierInformation(Guid Id)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ModelData = _setupService.GetCourier(Id);
            MobileRND_CourierInformation_VM viewModel = new MobileRND_CourierInformation_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateCourierInformation(MobileRND_CourierInformation_VM viewmodel)
        {
            MobileRND_SalesChannel_VM rt = new MobileRND_SalesChannel_VM();
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = _setupService.AddCourierInfo(viewmodel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "CourierInformation", null);
                }
                else
                {
                    ModelState.AddModelError("CourierName", "This Courier Already Exist!");
                }
            }
            return View(rt);
        }
    }
}

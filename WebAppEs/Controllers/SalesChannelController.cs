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
using WebAppEs.ViewModel.SalesChannel;

namespace WebAppEs.Controllers
{
    public class SalesChannelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;

        public SalesChannelController(
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

            var DataModel = _setupService.GetAllSalseChannelList();
            return View(DataModel);
        }

        public IActionResult CreateSalesChannel(Guid Id)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ModelData = _setupService.GetSalseChannel(Id);
            MobileRND_SalesChannel_VM viewModel = new MobileRND_SalesChannel_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateSalesChannel(MobileRND_SalesChannel_VM viewmodel)
        {
            MobileRND_SalesChannel_VM sc = new MobileRND_SalesChannel_VM();

            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = _setupService.AddSalesChannel(viewmodel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "SalesChannel", null);
                }
                else
                {
                    ModelState.AddModelError("ChannelName", "This Salse Channel Already Exist!");
                }
            }
            return View(viewmodel);
        }
    }
}

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
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.Controllers
{
    public class ZoneController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;

        public ZoneController(
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

            var DataModel = _setupService.GetAllZoneList();
            return View(DataModel);
        }

        public ActionResult CreateZone(Guid Id)
        {
            MobileRND_Zone_VM zone = new MobileRND_Zone_VM();
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ModelData = _setupService.GetZone(Id);
            MobileRND_Zone_VM viewModel = new MobileRND_Zone_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateZone(MobileRND_Zone_VM viewmodel)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                if(viewmodel.ZoneName == null || viewmodel.ZoneName == "")
                {
                    ModelState.AddModelError("ZoneName", "Zone Name Required!");
                }
                else
                {
                    var IsSubmit = _setupService.AddZone(viewmodel);
                    if (IsSubmit)
                    {
                        return RedirectToAction("Index", "Zone", null);
                    }
                    else
                    {
                        ModelState.AddModelError("ZoneName", "This Zone Name Already Exist!");
                    }
                }
                
            }
            return View(viewmodel);
        }
    }
}

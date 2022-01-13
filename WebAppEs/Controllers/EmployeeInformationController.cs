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
using WebAppEs.ViewModel.EmployeeInformation;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.Controllers
{
    public class EmployeeInformationController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ISetupService _setupService;

		public EmployeeInformationController(UserManager<ApplicationUser> userManager,
				RoleManager<IdentityRole> roleManager,
				ISetupService setupService, ILogger<HomeController> logger)
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

            var DataModel = _setupService.GetAllEmployeeList();
            return View(DataModel);
        }

        public IActionResult CreateEmployee(Guid Id)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ModelData = _setupService.GetEmployee(Id);
            MobileRND_EmployeeInformation_VM viewModel = new MobileRND_EmployeeInformation_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
                viewModel.IsUpdate = "Update";
            }

            viewModel.brandList = _setupService.GetAllBrand();
            viewModel.productList = _setupService.GetAllProduct();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateEmployee(MobileRND_EmployeeInformation_VM viewmodel)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = _setupService.AddEmployeeInfo(viewmodel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "EmployeeInformation", null);
                }
                else
                {
                    ModelState.AddModelError("EmployeeID", "This Employee Already Exist!");
                }
            }
            
            return View(viewmodel);
        }

        public IActionResult EmployeeAssignation()
        {
            MobileRND_AssignedEmployeeOrCustomer_VM viewModel = new MobileRND_AssignedEmployeeOrCustomer_VM();
            
            var channel = _setupService.GetAllSalseChannelList();
            if (channel.Count != 0)
            {
                viewModel.SalesChannelList = channel;
            }
            else
            {
                viewModel.SalesChannelList = new List<MobileRND_SalesChannel_VM>();
            }
            var zone = _setupService.GetAllZoneList();
            if (zone.Count != 0)
            {
                viewModel.ZoneList = zone;
            }
            else
            {
                viewModel.ZoneList = new List<MobileRND_Zone_VM>();
            }

            return View(viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult GetEmployeByType(string EmployeeType)
        {
            var result = _setupService.GetAllEmployeeListByType(EmployeeType);
            return Json(result);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult GetCustomerByChannelAndZone(string channel, string Zone)
        {
            Guid channelguid = Guid.Parse(channel);
            Guid Zoneguid = Guid.Parse(Zone);
            var result = _setupService.GetAllCustomerListByChannelAndZone(channelguid, Zoneguid);
            return Json(result);
        }
    }
}

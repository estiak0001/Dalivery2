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
using WebAppEs.ViewModel.Customer;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.Controllers
{
    public class CustomerInfoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;

        public CustomerInfoController(
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

            var DataModel = _setupService.GetAllCustomerList();
            return View(DataModel);
        }

        public IActionResult CreateCustomer(Guid Id)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var ModelData = _setupService.GetCustomer(Id);
            MobileRND_CustomerInfo_VM viewModel = new MobileRND_CustomerInfo_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
                viewModel.IsUpdate = "Update";
            }

            var channel = _setupService.GetAllSalseChannelList();
            if(channel.Count != 0)
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
            viewModel.brandList = _setupService.GetAllBrand();
            viewModel.productList = _setupService.GetAllProduct();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateCustomer(MobileRND_CustomerInfo_VM viewmodel)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = _setupService.AddCustomerInfo(viewmodel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "CustomerInfo", null);
                }
                else
                {
                    ModelState.AddModelError("CustomerName", "This Customer Already Exist!");
                }
            }

            var channel = _setupService.GetAllSalseChannelList();
            if (channel.Count != 0)
            {
                viewmodel.SalesChannelList = channel;
            }
            else
            {
                viewmodel.SalesChannelList = new List<MobileRND_SalesChannel_VM>();
            }
            return View(viewmodel);
        }

    }
}

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
using WebAppEs.ViewModel.Indexing;

namespace WebAppEs.Controllers
{
    public class ItemInformationController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDataAccessService _dataAccessService;
		private readonly ISetupService _setupService;
		private readonly ILogger<AdminController> _logger;

		public ItemInformationController(
				UserManager<ApplicationUser> userManager,
				RoleManager<IdentityRole> roleManager,
				IDataAccessService dataAccessService,
				ISetupService setupService,
				ILogger<AdminController> logger)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_dataAccessService = dataAccessService;
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
			var data = _setupService.GetAllItemsList();
			return View(data);
		}

		public IActionResult CreateItemInformation(Guid Id)
		{
			var employeeID = HttpContext.Session.GetString("EmployeeID");
			if (employeeID == null)
			{
				return RedirectToAction("Logout", "Account");
			}

			var ModelData = _setupService.GetItem(Id);
			MobileRND_Items_VM viewModel = new MobileRND_Items_VM();
			if (ModelData != null)
			{
				viewModel = ModelData;
			}
			viewModel.modelList = _setupService.GetAllPartsModelList();
			viewModel.brandlist = _setupService.GetAllBrand();
			return View(viewModel);
		}

        [HttpPost]
        //[Authorize("Roles")]
        public async Task<IActionResult> CreateItemInformation(MobileRND_Items_VM viewModel)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = await _setupService.AddItemInformation(viewModel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "ItemInformation", null);
                }
                else
                {
                    ModelState.AddModelError("ModelId", "This Model Already Exist!");
                }
            }
            return View(viewModel);
        }
    }
}
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
using WebAppEs.ViewModel.ProductModel;

namespace WebAppEs.Controllers
{
    public class ProductModelController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDataAccessService _dataAccessService;
		private readonly ISetupService _setupService;
		private readonly ILogger<AdminController> _logger;

		public ProductModelController(
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

			var DataModel = _setupService.GetAllPartsModelList();
			return View(DataModel);
		}

		public IActionResult CreateModel(Guid Id)
		{
			var employeeID = HttpContext.Session.GetString("EmployeeID");
			if (employeeID == null)
			{
				return RedirectToAction("Logout", "Account");
			}

			var ModelData = _setupService.GetPartsModelList(Id);
			ProductModel_VM viewModel = new ProductModel_VM();
			if (ModelData != null)
			{
				viewModel = ModelData;
			}
			return View(viewModel);
		}

		[HttpPost]
		//[Authorize("Roles")]
		public async Task<IActionResult> CreateModel(ProductModel_VM viewModel)
		{
			var employeeID = HttpContext.Session.GetString("EmployeeID");
			if (employeeID == null)
			{
				return RedirectToAction("Logout", "Account");
			}

			if (ModelState.IsValid)
			{
				var IsSubmit = await _setupService.AddPartsModel(viewModel);
				if (IsSubmit)
				{
					return RedirectToAction("Index", "ProductModel", null);
				}
				else
				{
					ModelState.AddModelError("Name", "This Model Already Exist!");
				}
			}
			return View(viewModel);
		}
	}
}


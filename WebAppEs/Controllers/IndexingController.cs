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
    public class IndexingController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IDataAccessService _dataAccessService;
		private readonly ISetupService _setupService;
		private readonly ILogger<AdminController> _logger;

		public IndexingController(
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

			var DataModel = _dataAccessService.GetAllIndexingDatalList();
			return View(DataModel);
		}

        public IActionResult CreateIndexing(Guid Id)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            var ModelData = _dataAccessService.GetIndexingData(Id);
            MobileRND_IndexingEntry_VM viewModel = new MobileRND_IndexingEntry_VM();
            if (ModelData != null)
            {
                viewModel = ModelData;
            }
            viewModel.itemlist = _setupService.GetAllItemsList();
            return View(viewModel);
        }

        [HttpPost]
        //[Authorize("Roles")]
        public async Task<IActionResult> CreateIndexing(MobileRND_IndexingEntry_VM viewModel)
        {
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            if (employeeID == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            if (ModelState.IsValid)
            {
                var IsSubmit = await _dataAccessService.AddIndexingData(viewModel);
                if (IsSubmit)
                {
                    return RedirectToAction("Index", "Indexing", null);
                }
                else
                {
                    //ModelState.AddModelError("Name", "This Model Already Exist!");
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult LoadIndexingDataItemWise(string ItemId, string Brand, string Model, string Grade)
        {
            Guid GuidItemId = ItemId == null ? Guid.Empty : Guid.Parse(ItemId);
            Guid GuidBrand = Brand == null ? Guid.Empty : Guid.Parse(Brand);
            Guid GuidModel = Model == null ? Guid.Empty : Guid.Parse(Model);
            string Gradetxt = Grade == null ? "" : Grade;

            var result = _dataAccessService.GetIndexingDataItemWise(GuidItemId, GuidBrand, GuidModel, Gradetxt);

            return Json(result);
        }

        public IActionResult Preview()
        {
            PreviewModel preview = new PreviewModel();
            preview.brandlist = _setupService.GetAllBrand();
            preview.modelList = _setupService.GetAllPartsModelList();
            preview.itemlist = _setupService.GetAllItemsList();
            return View(preview);
        }
    }
}

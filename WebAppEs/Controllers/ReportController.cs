using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.Models;
using WebAppEs.Services;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.ReportPannel;

namespace WebAppEs.Controllers
{
    public class ReportController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;
        private readonly IDataAccessService _dataAccessService;

        public ReportController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IDataAccessService dataAccessService,
                ISetupService setupService,
                ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _setupService = setupService;
            _dataAccessService = dataAccessService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Preview()
        {
            Preview viewModel = new Preview();
            viewModel.paymentTypeList = _setupService.GetAllPaymentType();
            viewModel.CustomerList = _setupService.GetAllCustomerList();
            viewModel.CourierList = _setupService.GetAllCourierList();

            //_dataAccessService.PreviewReportData();
            return View(viewModel);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult GetPreviewData(DateTime? FromDate, DateTime? ToDate, string PaymentType, string CourierID, string Status, string CoustomerID)
        {
            Guid GPaymentType = PaymentType == null ? Guid.Empty : Guid.Parse(PaymentType);
            Guid GCourierID = CourierID == null ? Guid.Empty : Guid.Parse(CourierID);
            Guid GCoustomerID = CoustomerID == null ? Guid.Empty : Guid.Parse(CoustomerID);
            var result = _dataAccessService.PreviewReportData(FromDate, ToDate, GPaymentType, GCourierID, Status, GCoustomerID);
            return Json(result);
        }
    }
}

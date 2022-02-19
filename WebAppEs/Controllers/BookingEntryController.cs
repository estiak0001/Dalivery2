using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppEs.Models;
using WebAppEs.Services;
using WebAppEs.ViewModel.Booking;

namespace WebAppEs.Controllers
{
    public class BookingEntryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISetupService _setupService;
        private readonly ILogger<AdminController> _logger;
        private readonly IDataAccessService _dataAccessService;

        public BookingEntryController(
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
            var data = _dataAccessService.BookingList();
            return View(data);
        }

        public IActionResult CreateBooking(Guid Id)
        {
            MobileRND_BookingEntry_VM viewModel = new MobileRND_BookingEntry_VM();

            if (Id == Guid.Empty)
            {
                viewModel.paymentTypeList = _setupService.GetAllPaymentType();
                viewModel.brandList = _setupService.GetAllBrand();
                viewModel.productList = _setupService.GetAllProduct();
                viewModel.courierList = _setupService.GetAllCourierList();
                return View(viewModel);
            }
            else
            {
                viewModel = _dataAccessService.BookingHeadByID(Id);
                viewModel.bookingDetails = _dataAccessService.BokkingDetailByHead(Id);
                viewModel.paymentTypeList = _setupService.GetAllPaymentType();
                viewModel.brandList = _setupService.GetAllBrand();
                viewModel.productList = _setupService.GetAllProduct();
                viewModel.courierList = _setupService.GetAllCourierList();
                return View(viewModel);
            }
            
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult GetRate(string CourierID, string Couriertype)
        {
            Guid couID = Guid.Parse(CourierID);
            var result = _setupService.GetCourierRateByCourierIDAndType(couID, Couriertype);
            return Json(result);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult GetCustomer(string CustomerNo)
        {
            var result = _setupService.GetCustomerByCustomerNo(CustomerNo);
            return Json(result);
        }


        public JsonResult LoadIsExistDaa(DateTime Date, string PaymentType, string Courier, string Brand, string product)
        {
        

        Guid GuidPaymentType = PaymentType == null ? Guid.Empty : Guid.Parse(PaymentType);
            Guid GuidCourier = Courier == null ? Guid.Empty : Guid.Parse(Courier);
            Guid GuidBrand = Brand == null ? Guid.Empty : Guid.Parse(Brand);
            Guid Guidproduct = product == null ? Guid.Empty : Guid.Parse(product);
            var result = _dataAccessService.BookingDatabyHead(Date, GuidPaymentType, GuidCourier, GuidBrand, Guidproduct);
            MobileRND_BookingEntry_VM headWiseData = new MobileRND_BookingEntry_VM();
            if (result != null)
            {
                headWiseData = result;
                headWiseData.bookingDetails = _dataAccessService.BokkingDetailByHead(result.Id);
            }
            return Json(headWiseData);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult BookingDataEntry([FromBody] MobileRND_BookingEntry_VM model)
        {
            ClaimsPrincipal currentUser = this.User;
            var ID = HttpContext.Session.GetString("Id");
            Guid newGuid = Guid.Parse(ID);
            bool isSuperAdmin = currentUser.IsInRole("SuperAdmin");
            bool isAdmin = currentUser.IsInRole("Admin");

            var HedSubmit = false;
            var DetailsSubmit = false;
            //bool status = false;
            StatusModel status = new StatusModel();
            status.success = false;
            var employeeID = HttpContext.Session.GetString("EmployeeID");
            model.LUser = newGuid;

            if (ModelState.IsValid)
            {
                var head = _dataAccessService.BookingDatabyHead(model.BookingDate, model.PaymentTypeId, model.CourierId, model.BrandID, model.ProductID);
                if (head == null)
                {
                    HedSubmit = _dataAccessService.AddBookingEntry(model);
                    if (HedSubmit)
                    {
                        var Savedhead = _dataAccessService.BookingDatabyHead(model.BookingDate, model.PaymentTypeId, model.CourierId, model.BrandID, model.ProductID);

                        foreach (var it in model.bookingDetails)
                        {
                            it.BookingId = Savedhead.Id;
                            it.LUser = newGuid;
                            var IsSubmit = _dataAccessService.AddBookingDetails(it);
                        }
                    }
                }
                else
                {
                    //var details = _dataAccessService.AllDataByBookingID(head.Id);
                    //if (details != null)
                    //{
                    //    var isDelete = _dataAccessService.RemoveDetails(details);
                    //}

                    foreach (var it in model.bookingDetails)
                    {
                        var isExist = _dataAccessService.BookingDataByCN(it.CNNumber);
                        if (isExist == null)
                        {
                            it.BookingId = head.Id;
                            it.LUser = newGuid;

                            DetailsSubmit = _dataAccessService.AddBookingDetails(it);
                        }
                    }
                }
            }
            else
            {
                status.success = false;
            }

            if (DetailsSubmit)
            {
                status.success = true;
            }

            return Json(status);
        }

    }
}

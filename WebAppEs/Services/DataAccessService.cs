using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAppEs.Data;
using WebAppEs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Home;
using WebAppEs.ViewModel.Register;
using WebAppEs.ViewModel.Booking;
using WebAppEs.Entity;
using WebAppEs.ViewModel.ReportPannel;
using WebAppEs.ViewModel.Indexing;

namespace WebAppEs.Services
{
    public class DataAccessService : IDataAccessService
	{
		private readonly IMemoryCache _cache;
		private readonly ApplicationDbContext _context;
		private readonly ISetupService _setupService;
		public DataAccessService(ApplicationDbContext context, IMemoryCache cache, ISetupService setupService)
		{
			_cache = cache;
			_context = context;
			_setupService = setupService;
		}

		public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
		{
			var isAuthenticated = principal.Identity.IsAuthenticated;
			if (!isAuthenticated)
			{
				return new List<NavigationMenuViewModel>();
			}

			var roleIds = await GetUserRoleIds(principal);

			var permissions = await _cache.GetOrCreateAsync("Permissions",
				async x => await (from menu in _context.NavigationMenu select menu).ToListAsync());

			var rolePermissions = await _cache.GetOrCreateAsync("RolePermissions",
				async x => await (from menu in _context.RoleMenuPermission select menu).Include(x => x.NavigationMenu).ToListAsync());

			var data = (from menu in rolePermissions
						join p in permissions on menu.NavigationMenuId equals p.Id
						where roleIds.Contains(menu.RoleId)
						select p)
							  .Select(m => new NavigationMenuViewModel()
							  {
								  Id = m.Id,
								  Name = m.Name,
								  Area = m.Area,
								  Visible = m.Visible,
								  IsExternal = m.IsExternal,
								  ActionName = m.ActionName,
								  ExternalUrl = m.ExternalUrl,
								  DisplayOrder = m.DisplayOrder,
								  ParentMenuId = m.ParentMenuId,
								  ControllerName = m.ControllerName,
							  }).Distinct().OrderBy(x=> x.DisplayOrder).ToList();

			return data;
		}

		public async Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act)
		{
			var result = false;
			var roleIds = await GetUserRoleIds(ctx);
			var data = await (from menu in _context.RoleMenuPermission
							  where roleIds.Contains(menu.RoleId)
							  select menu)
							  .Select(m => m.NavigationMenu)
							  .Distinct()
							  .ToListAsync();

			foreach (var item in data)
			{
				result = (item.ControllerName == ctrl && item.ActionName == act);
				if (result)
				{
					break;
				}
			}

			return result;
		}

		public async Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id)
		{
			var items = await (from m in _context.NavigationMenu
							   join rm in _context.RoleMenuPermission
								on new { X1 = m.Id, X2 = id } equals new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
								into rmp
							   from rm in rmp.DefaultIfEmpty()
							   select new NavigationMenuViewModel()
							   {
								   Id = m.Id,
								   Name = m.Name,
								   Area = m.Area,
								   ActionName = m.ActionName,
								   ControllerName = m.ControllerName,
								   IsExternal = m.IsExternal,
								   ExternalUrl = m.ExternalUrl,
								   DisplayOrder = m.DisplayOrder,
								   ParentMenuId = m.ParentMenuId,
								   Visible = m.Visible,
								   Permitted = rm.RoleId == id
							   })
							   .AsNoTracking()
							   .ToListAsync();

			return items;
		}

		public async Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds)
		{
			var existing = await _context.RoleMenuPermission.Where(x => x.RoleId == id).ToListAsync();
			_context.RemoveRange(existing);

			foreach (var item in permissionIds)
			{
				await _context.RoleMenuPermission.AddAsync(new RoleMenuPermission()
				{
					RoleId = id,
					NavigationMenuId = item,
				});
			}

			var result = await _context.SaveChangesAsync();

			// Remove existing permissions to roles so it can re evaluate and take effect
			_cache.Remove("RolePermissions");

			return result > 0;
		}

		private async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
		{
			var userId = GetUserId(ctx);
			var data = await (from role in _context.UserRoles
							  where role.UserId == userId
							  select role.RoleId).ToListAsync();
			return data;
		}

		private string GetUserId(ClaimsPrincipal user)
		{
			return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}


        public List<EmployeeListVM> GetAllEmployeeList()
        {
			var items = (from user in _context.Users

						 select new EmployeeListVM()
						 {
							 EmployeeID = user.EmployeeID,
							 EmployeeName = user.Name + " ("+ user.EmployeeID+")"
						 }).ToList();
			return items;
		}

        public MobileRND_BookingEntry_VM BookingDatabyHead(DateTime? sortdate, Guid PaymentTypeId, Guid CourierId, Guid BrandID, Guid ProductID)
        {
			var items = (from booking in _context.MobileRND_BookingEntry.Where(x => x.BookingDate == sortdate && x.PaymentTypeId == PaymentTypeId && x.CourierId == CourierId && x.BrandID == BrandID && x.ProductID == ProductID)					 
						 select new MobileRND_BookingEntry_VM()
						 {
							 Id = booking.Id,
							 PaymentTypeId = booking.PaymentTypeId,
							 BookingDate = booking.BookingDate,
							 DateString = String.Format("{0:MM/dd/yyyy}", booking.BookingDate),
							 
						 }).FirstOrDefault();
			return items;
		}

        public bool AddBookingEntry(MobileRND_BookingEntry_VM viewModel)
        {
			if (viewModel == null)
			{
				return false;
			}
			else
			{
				_context.MobileRND_BookingEntry.Add(new MobileRND_BookingEntry()
				{
					BookingDate = viewModel.BookingDate,
					PaymentTypeId = viewModel.PaymentTypeId,
					CourierId = viewModel.CourierId,
					BrandID = viewModel.BrandID,
					ProductID = viewModel.ProductID,
					//IsApprove = false,
					LUser = viewModel.LUser
				});
			}
			var result = _context.SaveChanges();
			return result > 0;
		}

        public bool AddBookingDetails(MobileRND_BookingDetailsEntry_VM viewModel)
        {
			if (viewModel == null)
			{
				return false;
			}
			else
			{
				_context.MobileRND_BookingDetailsEntry.Add(new MobileRND_BookingDetailsEntry()
				{
					BookingId = viewModel.BookingId,
					CNNumber = viewModel.CNNumber,
					Quantity = viewModel.Quantity,
					Ammount = viewModel.Ammount,
					Rate = Convert.ToDecimal(viewModel.Rate),
					CourierType = viewModel.CourierType,
					CustomerNo = viewModel.CustomerNo,
					Remarks = viewModel.Remarks,
					DoNo = viewModel.DoNo,
					UpdatedOn = DateTime.Today,
					LUser = viewModel.LUser
				});
			}
			var result = _context.SaveChanges();

			return result > 0;
		}

        public List<MobileRND_BookingDetailsEntry> AllDataByBookingID(Guid Id)
        {
			var data = _context.MobileRND_BookingDetailsEntry.Where(x => x.BookingId == Id).ToList();
			return data;
		}

        public bool RemoveDetails(List<MobileRND_BookingDetailsEntry> Model)
        {
			_context.MobileRND_BookingDetailsEntry.RemoveRange(Model);
			var result = _context.SaveChanges();

			if (result == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

        public List<MobileRND_BookingEntry_VM> BookingList()
        {
			var items = (from booking in _context.MobileRND_BookingEntry
                         join payment in _context.MobileRND_PaymentType
                            on new { X1 = booking.PaymentTypeId } equals new { X1 = payment.Id }
                            into rmp
                         from rm in rmp.DefaultIfEmpty()
						 join courier in _context.MobileRND_CourierInformation
							on new { X1 = booking.CourierId } equals new { X1 = courier.Id }
							into rmpcu
						 from rmcu in rmpcu.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = booking.BrandID } equals new { X1 = brand.Id }
							into rmpbr
						 from rmbr in rmpbr.DefaultIfEmpty()
						 join product in _context.MobileRND_Product
							on new { X1 = booking.ProductID } equals new { X1 = product.Id }
							into rmppr
						 from rmpr in rmppr.DefaultIfEmpty()
						 //join details in _context.MobileRND_BookingDetailsEntry
							//on new { X1 = booking.Id } equals new { X1 = details.BookingId }
							//into ddd
						 //from dd in ddd.DefaultIfEmpty()
						 select new MobileRND_BookingEntry_VM()
						 {
							 Id = booking.Id,
							 CourierId = booking.CourierId,
							 BookingDate = booking.BookingDate,
							 DateString = String.Format("{0:MM/dd/yyyy}", booking.BookingDate),
							 StatusIsToday = booking.BookingDate == DateTime.Today ? true : false,
							 BrandID = booking.BrandID,
							 ProductID = booking.ProductID,
							 PaymentTypeId = booking.PaymentTypeId,
							 PaymentName = rm.TypeName,
							 Courier = rmcu.CourierName,
							 Brand = rmbr.BrandName,
							 Product = rmpr.ProductName,
							 TotalQuantity = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.Quantity).Sum(),
							 TotalAmount = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.Ammount).Sum(),
							 TotalCN = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.CNNumber).Count(),
						 }).ToList();
			return items;
		}

        public MobileRND_BookingEntry_VM BookingHeadByID(Guid Id)
        {
			var items = (from booking in _context.MobileRND_BookingEntry.Where(x => x.Id == Id)
						 
						 select new MobileRND_BookingEntry_VM()
						 {
							 Id = booking.Id,
							 PaymentTypeId = booking.PaymentTypeId,
							 BookingDate = booking.BookingDate,
							 CourierId = booking.CourierId,
							 BrandID = booking.BrandID,
							 ProductID = booking.ProductID,
						 }).FirstOrDefault();
			return items;
		}

        public List<MobileRND_BookingDetailsEntry_VM> BokkingDetailByHead(Guid BookingId)
        {
			var items = (from details in _context.MobileRND_BookingDetailsEntry.Where(x=> x.BookingId == BookingId)
						 join cus in _context.MobileRND_CustomerInfo
							on new { X1 = details.CustomerNo } equals new { X1 = cus.CustomerNo }
							into rmppr
						 from rmpr in rmppr.DefaultIfEmpty()
						 select new MobileRND_BookingDetailsEntry_VM()
						 {
							 Id = details.Id,
							 CNNumber = details.CNNumber,
							 Quantity = details.Quantity,
							 Ammount = details.Ammount,
							 Rate = details.Rate,
							 DoNo = details.DoNo,
							 CustomerNameWithNo = rmpr.CustomerName + " (" + details.CustomerNo + ")",
							 CustomerNo =  details.CustomerNo,
							 Remarks = details.Remarks,
							 CourierType = details.CourierType,
						 }).ToList();
			return items;
		}

        public List<PreviewDataModel> PreviewReportData(DateTime? FromDate, DateTime? ToDate, Guid PaymentType, Guid CourierID, string Status, Guid CoustomerID)
        {

			var items = (from booking in _context.MobileRND_BookingEntry
						 join payment in _context.MobileRND_PaymentType
							on new { X1 = booking.PaymentTypeId } equals new { X1 = payment.Id }
							into rmp
						 from rm in rmp.DefaultIfEmpty()
						 join courier in _context.MobileRND_CourierInformation
							on new { X1 = booking.CourierId } equals new { X1 = courier.Id }
							into rmpcu
						 from rmcu in rmpcu.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = booking.BrandID } equals new { X1 = brand.Id }
							into rmpbr
						 from rmbr in rmpbr.DefaultIfEmpty()
						 join product in _context.MobileRND_Product
							on new { X1 = booking.ProductID } equals new { X1 = product.Id }
							into rmppr
						 from rmpr in rmppr.DefaultIfEmpty()

						 join details in _context.MobileRND_BookingDetailsEntry
							on new { X1 = booking.Id } equals new { X1 = details.BookingId }
							into rmde
						 from rmd in rmde.DefaultIfEmpty()


						 join customer in _context.MobileRND_CustomerInfo
							on new { X1 = rmd.CustomerNo } equals new { X1 = customer.CustomerNo }
							into rmcc
						 from rmc in rmcc.DefaultIfEmpty()

						 select new PreviewDataModel()
						 {
							 BookingDate = booking.BookingDate,
							 BookingDateString = String.Format("{0:MM/dd/yyyy}", booking.BookingDate),
							 CustomerNameWithID = rmc.CustomerName + " ("+ rmc.CustomerNo +")",
							 CourierName = rmcu.CourierName,
							 Area = rmd.CourierType,
							 Brand = rmbr.BrandName,
							 CNNumber = rmd.CNNumber,
							 Quantity = rmd.Quantity,
							 Rate = rmd.Rate,
							 DoNo = rmd.DoNo,
							 Amout = rmd.Ammount,
							 Remarks = rmd.Remarks,
							 Status = rmd.IsDelivered == true ? "Delivered" : "Undelivered",
							 StatusSort = rmd.IsDelivered == true ? "yes" : "no",
							 DeliveredDateTime = rmd.DeliveredDateTime == null ? "" : String.Format("{0:MM/dd/yyyy h:mm tt}", rmd.DeliveredDateTime),
							 CourierID = booking.CourierId,
							 CustomerId = rmc.Id,
							 PaymentType = booking.PaymentTypeId
						 }).Where(x=> ((FromDate == null || x.BookingDate >= FromDate) && (ToDate == null || x.BookingDate <= ToDate)) && (PaymentType == Guid.Empty || x.PaymentType == PaymentType) && (CourierID == Guid.Empty || x.CourierID == CourierID)
							&& (PaymentType == Guid.Empty || x.PaymentType == PaymentType) && (CourierID == Guid.Empty || x.CourierID == CourierID) && (Status == "" || Status == null || x.StatusSort == Status) && (CoustomerID == Guid.Empty || x.CustomerId == CoustomerID)).ToList();
			return items;
		}

        public async Task<bool> AddIndexingData(MobileRND_IndexingEntry_VM viewModel)
        {
			var result = 0;
			var UpdateDataSet = await _context.MobileRND_IndexingEntry.Where(x => x.Id == viewModel.Id).FirstOrDefaultAsync();

			if (UpdateDataSet != null)
			{
				UpdateDataSet.ItemId = viewModel.ItemId;
				UpdateDataSet.Grade = viewModel.Grade;
				UpdateDataSet.Block = viewModel.Block;
				UpdateDataSet.RackNo = viewModel.RackNo;
				UpdateDataSet.StageNo = viewModel.StageNo;
				UpdateDataSet.ColumNo = viewModel.ColumNo;

				UpdateDataSet.IndexingDate = (DateTime)viewModel.IndexingDate;
				UpdateDataSet.Remarks = viewModel.Remarks;

				_context.MobileRND_IndexingEntry.Update(UpdateDataSet);
				result = await _context.SaveChangesAsync();
			}
			else
			{
				_context.MobileRND_IndexingEntry.Add(new MobileRND_IndexingEntry()
				{
					ItemId = viewModel.ItemId,
					Grade = viewModel.Grade,
					Block = viewModel.Block,
					RackNo = viewModel.RackNo,
					StageNo = viewModel.StageNo,
					ColumNo = viewModel.ColumNo,
					IndexingDate = (DateTime)viewModel.IndexingDate,
					Remarks = viewModel.Remarks
				});
				result = await _context.SaveChangesAsync();
			}
			return result > 0;
		}

        public List<MobileRND_IndexingEntry_VM> GetAllIndexingDatalList()
        {
			var items = (from ind in _context.MobileRND_IndexingEntry
						 join it in _context.MobileRND_Items
							on new { X1 = ind.ItemId } equals new { X1 = it.Id }
							into ittt
						 from itttt in ittt.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = itttt.Brand } equals new { X1 = brand.Id }
							into br
						 from brr in br.DefaultIfEmpty()
						 join model in _context.MobileRND_ProductModel
							on new { X1 = itttt.ModelId } equals new { X1 = model.ID }
							into mo
						 from moo in mo.DefaultIfEmpty()
						 select new MobileRND_IndexingEntry_VM()
						 {
							 Id = ind.Id,
							 IndexingDate = ind.IndexingDate,
							 IndexingDateString = String.Format("{0:MM/dd/yyyy}", ind.IndexingDate),
							 BrandName = brr.BrandName,
							 ModelName = moo.Name,
							 ItemCode = itttt.ItemCode,
							 ItemName = itttt.ItemName,
							 ItemId = itttt.Id,
							 Grade = ind.Grade,
							 Block = "Block-"+ind.Block,
							 RackNo = "Rack-"+ind.RackNo,
							 StageNo = "Stage-"+ind.StageNo,
							 ColumNo = "Colum-"+ind.ColumNo,
							 Remarks = ind.Remarks,
						 }).ToList();
			return items;
		}

		public MobileRND_IndexingEntry_VM GetIndexingData(Guid Id)
        {
			var items = (from ind in _context.MobileRND_IndexingEntry.Where(x=> x.Id == Id)
						 join it in _context.MobileRND_Items
							on new { X1 = ind.ItemId } equals new { X1 = it.Id }
							into ittt
						 from itttt in ittt.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = itttt.Brand } equals new { X1 = brand.Id }
							into br
						 from brr in br.DefaultIfEmpty()
						 join model in _context.MobileRND_ProductModel
							on new { X1 = itttt.ModelId } equals new { X1 = model.ID }
							into mo
						 from moo in mo.DefaultIfEmpty()
						 select new MobileRND_IndexingEntry_VM()
						 {
							 Id = ind.Id,
							 IndexingDate = ind.IndexingDate,
							 IndexingDateString = String.Format("{0:MM/dd/yyyy}", ind.IndexingDate),
							 BrandName = brr.BrandName,
							 ModelName = moo.Name,
							 ItemCode = itttt.ItemCode,
							 ItemName = itttt.ItemName,
							 ItemId = itttt.Id,
							 Grade = ind.Grade,
							 Block = ind.Block,
							 RackNo = ind.RackNo,
							 StageNo = ind.StageNo,
							 ColumNo = ind.ColumNo,
							 Remarks = ind.Remarks,
						 }).FirstOrDefault();
			return items;
		}

		public List<MobileRND_IndexingEntry_VM> GetIndexingDataItemWise(Guid ItemId, Guid Brand, Guid Model, string Grade)
		{
			var items = (from ind in _context.MobileRND_IndexingEntry
						 join it in _context.MobileRND_Items
							on new { X1 = ind.ItemId } equals new { X1 = it.Id }
							into ittt
						 from itttt in ittt.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = itttt.Brand } equals new { X1 = brand.Id }
							into br
						 from brr in br.DefaultIfEmpty()
						 join model in _context.MobileRND_ProductModel
							on new { X1 = itttt.ModelId } equals new { X1 = model.ID }
							into mo
						 from moo in mo.DefaultIfEmpty()
						 select new MobileRND_IndexingEntry_VM()
						 {
							 Id = ind.Id,
							 IndexingDate = ind.IndexingDate,
							 IndexingDateString = String.Format("{0:MM/dd/yyyy}", ind.IndexingDate),
							 BrandName = brr.BrandName,
							 ModelName = moo.Name,
							 ItemCode = itttt.ItemCode,
							 ItemName = itttt.ItemName,
							 ItemId = itttt.Id,
							 Grade = ind.Grade,
							 Block = "Block-" + ind.Block,
							 RackNo = "Rack-" + ind.RackNo,
							 StageNo = "Stage-" + ind.StageNo,
							 ColumNo = "Colum-" + ind.ColumNo,
							 Remarks = ind.Remarks,
							 BrandId = itttt.Brand,
							 ModelId = moo.ID
						 }).Where(x => (ItemId == Guid.Empty || x.ItemId == ItemId) && (Brand == Guid.Empty || x.BrandId == Brand) && (Model == Guid.Empty || x.ModelId == Model) && (Grade == "" || x.Grade == Grade)).ToList();
			return items;
		}

        public List<DailySummeryReport_VM> DailySummeryReport(DateTime? FromDate, DateTime? ToDate, Guid BrandId)
        {
			//var rt = from ind in _context.MobileRND_BookingEntry
			//		 join it in _context.MobileRND_BookingDetailsEntry
			//				 on new { X1 = ind.Id } equals new { X1 = it.BookingId }
			//				 into ittt
			//		 from itttt in ittt.DefaultIfEmpty()
			//		 select new(pi => ind.Quantity).Sum();

			//		var t = from ind in _context.MobileRND_BookingEntry
			//				join it in _context.MobileRND_BookingDetailsEntry
			//				 on new { X1 = ind.Id } equals new { X1 = it.BookingId }
			//				 into ittt
			//				from itttt in ittt.DefaultIfEmpty()
			//				select new
			//				{
			//					qu = itttt.Quantity
			//				};

			string iDate = "01/12/2022";
			DateTime oDate = Convert.ToDateTime(iDate);

			var items = (from booking in _context.MobileRND_BookingEntry
						 join payment in _context.MobileRND_PaymentType
							on new { X1 = booking.PaymentTypeId } equals new { X1 = payment.Id }
							into rmp
						 from rm in rmp.DefaultIfEmpty()
						 join courier in _context.MobileRND_CourierInformation
							on new { X1 = booking.CourierId } equals new { X1 = courier.Id }
							into rmpcu
						 from rmcu in rmpcu.DefaultIfEmpty()
						 join brand in _context.MobileRND_Brand
							on new { X1 = booking.BrandID } equals new { X1 = brand.Id }
							into rmpbr
						 from rmbr in rmpbr.DefaultIfEmpty()
						 join product in _context.MobileRND_Product
							on new { X1 = booking.ProductID } equals new { X1 = product.Id }
							into rmppr
						 from rmpr in rmppr.DefaultIfEmpty()
							 //join details in _context.MobileRND_BookingDetailsEntry
							 //on new { X1 = booking.Id } equals new { X1 = details.BookingId }
							 //into ddd
							 //from dd in ddd.DefaultIfEmpty()
						 select new DailySummeryReport_VM()
						 {
							 Id = booking.Id,
							 CourierId = booking.CourierId,
							 BookingDate = booking.BookingDate,
							 DateString = String.Format("{0:MM/dd/yyyy}", booking.BookingDate),
							 BrandID = booking.BrandID,
							 ProductID = booking.ProductID,
							 PaymentTypeId = booking.PaymentTypeId,
							 PaymentName = rm.TypeName,
							 Courier = rmcu.CourierName,
							 Brand = rmbr.BrandName,
							 Product = rmpr.ProductName,

							 TotalQuantity = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.Quantity).Sum(),
							 TotalAmount = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.Ammount).Sum(),
							 TotalCN = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.CNNumber).Count(),

							 //TotalQuantity = _context.MobileRND_BookingDetailsEntry.Where(pi => pi. == booking.Id).Select(pi => pi.Quantity).Sum(),
							 //TotalAmount = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.Ammount).Sum(),
							 //TotalCN = _context.MobileRND_BookingDetailsEntry.Where(pi => pi.BookingId == booking.Id).Select(pi => pi.CNNumber).Count(),
						 }).Where(x=> x.BrandID == BrandId).ToList();

			var courierlist = _setupService.GetAllCourierList();



			return items;
		}

        public MobileRND_BookingDetailsEntry BookingDataByCN(string CNNO)
        {
			var data = _context.MobileRND_BookingDetailsEntry.Where(x => x.CNNumber == CNNO).FirstOrDefault();
			return data;
		}
    }
}
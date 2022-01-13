using WebAppEs.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Home;
using WebAppEs.ViewModel.Register;
using WebAppEs.ViewModel.Booking;
using WebAppEs.Entity;

namespace WebAppEs.Services
{
    public interface IDataAccessService
	{
		Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act);
		Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal);
		Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id);
		Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds);
		List<EmployeeListVM> GetAllEmployeeList();

		//Booking
		MobileRND_BookingEntry_VM BookingDatabyHead(DateTime? sortdate, Guid PaymentTypeId, Guid CourierId, Guid BrandID, Guid ProductID);
		bool AddBookingEntry(MobileRND_BookingEntry_VM viewModel);
		bool AddBookingDetails(MobileRND_BookingDetailsEntry_VM viewModel);
		List<MobileRND_BookingDetailsEntry> AllDataByBookingID(Guid Id);
		bool RemoveDetails(List<MobileRND_BookingDetailsEntry> Model);

		List<MobileRND_BookingEntry_VM> BookingList();

		MobileRND_BookingEntry_VM BookingHeadByID(Guid Id);
		List<MobileRND_BookingDetailsEntry_VM> BokkingDetailByHead(Guid BookingId);
	}
}
﻿using WebAppEs.Models;
using System;
using System.Collections.Generic;
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
		MobileRND_BookingDetailsEntry BookingDataByCN(string CNNO);
		bool RemoveDetails(List<MobileRND_BookingDetailsEntry> Model);

		List<MobileRND_BookingEntry_VM> BookingList();

		MobileRND_BookingEntry_VM BookingHeadByID(Guid Id);
		List<MobileRND_BookingDetailsEntry_VM> BokkingDetailByHead(Guid BookingId);
        List<PreviewDataModel> PreviewReportData(DateTime? FromDate, DateTime? ToDate, Guid PaymentType, Guid CourierID, string Status, Guid CoustomerID);

        //Indexing
        Task<bool> AddIndexingData(MobileRND_IndexingEntry_VM viewModel);
        List<MobileRND_IndexingEntry_VM> GetAllIndexingDatalList();
		MobileRND_IndexingEntry_VM GetIndexingData(Guid Id);
		List<MobileRND_IndexingEntry_VM> GetIndexingDataItemWise(Guid ItemId, Guid Brand, Guid Model, string Grade);

		//Report
		List<DailySummeryReport_VM> DailySummeryReport(DateTime? FromDate, DateTime? ToDate, Guid BrandId);
	}
}
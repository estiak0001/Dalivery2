using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.Courier;
using WebAppEs.ViewModel.Customer;
using WebAppEs.ViewModel.EmployeeInformation;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.Services
{
    public interface ISetupService
    {
        //Sales Channel

        bool AddSalesChannel(MobileRND_SalesChannel_VM viewModel);
        List<MobileRND_SalesChannel_VM> GetAllSalseChannelList();
        MobileRND_SalesChannel_VM GetSalseChannel(Guid Id);

        //Zone

        bool AddZone(MobileRND_Zone_VM viewModel);
        List<MobileRND_Zone_VM> GetAllZoneList();
        MobileRND_Zone_VM GetZone(Guid Id);

        //Customer

        bool AddCustomerInfo(MobileRND_CustomerInfo_VM viewModel);
        List<MobileRND_CustomerInfo_VM> GetAllCustomerList();
        List<MobileRND_CustomerInfo_VM> GetAllCustomerListByChannelAndZone(Guid channel, Guid Zone);
        MobileRND_CustomerInfo_VM GetCustomer(Guid Id);

        //Customer

        bool AddCourierInfo(MobileRND_CourierInformation_VM viewModel);
        List<MobileRND_CourierInformation_VM> GetAllCourierList();
        MobileRND_CourierInformation_VM GetCourier(Guid Id);

        //Employee

        bool AddEmployeeInfo(MobileRND_EmployeeInformation_VM viewModel);
        List<MobileRND_EmployeeInformation_VM> GetAllEmployeeList();
        MobileRND_EmployeeInformation_VM GetEmployee(Guid Id);
        List<MobileRND_EmployeeInformation_VM> GetAllEmployeeListByType(string EmployeeType);

        //PaymentType
        List<MobileRND_PaymentType_VM> GetAllPaymentType();
        List<MobileRND_Brand_VM> GetAllBrand();
        List<MobileRND_Product_VM> GetAllProduct();


        //Booking
        GetCourierRate GetCourierRateByCourierIDAndType(Guid CourierID, string Couriertype);
        MobileRND_CustomerInfo_VM GetCustomerByCustomerNo(string CustomerNo);

    }
}

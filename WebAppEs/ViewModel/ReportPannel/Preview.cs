using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.Courier;
using WebAppEs.ViewModel.Customer;

namespace WebAppEs.ViewModel.ReportPannel
{
    public class Preview
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string IsDelivered { get; set; }
        public Guid CourierID { get; set; }
        public Guid PaymentType { get; set; }
        public Guid CustomerId { get; set; }
        public List<MobileRND_CustomerInfo_VM> CustomerList { get; set; }
        public List<MobileRND_CourierInformation_VM> CourierList { get; set; }
        public List<MobileRND_PaymentType_VM> paymentTypeList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.ViewModel.ReportPannel
{
    public class PreviewDataModel
    {
        public DateTime? BookingDate { get; set; }
        public string BookingDateString { get; set; }
        public string CustomerNameWithID { get; set; }
        public string CourierName { get; set; }
        public string Area { get; set; }
        public string Brand { get; set; }
        public string CNNumber { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amout { get; set; }
        public string Status { get; set; }
        public string StatusSort { get; set; }
        public string Remarks { get; set; }
        public string DeliveredDateTime { get; set; }
        public string DoNo { get; set; }
        //sort

        public Guid CourierID { get; set; }
        public Guid PaymentType { get; set; }
        public Guid CustomerId { get; set; }
    }
}

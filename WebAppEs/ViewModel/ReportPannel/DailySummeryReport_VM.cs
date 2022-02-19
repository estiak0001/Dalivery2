using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.ViewModel.ReportPannel
{
    public class DailySummeryReport_VM
    {
        public Guid Id { get; set; }
        public DateTime? BookingDate { get; set; }
        public string DateString { get; set; }
        public Guid PaymentTypeId { get; set; }
        public string PaymentName { get; set; }
        public Guid CourierId { get; set; }
        public string Courier { get; set; }
        //public decimal rate { get; set; }
        public string CourierType { get; set; }

        public Guid BrandID { get; set; }
        public Guid ProductID { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }


        public int CN { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public int TotalCN { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

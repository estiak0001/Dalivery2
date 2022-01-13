using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.ViewModel.Customer
{
    public class MobileRND_CustomerInfo_VM
    {
        public Guid Id { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public Guid SalesChannelID { get; set; }
        public string SalseChannelName { get; set; }
        public Guid ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string Product { get; set; }
        public string Brand { get; set; }
        public Guid ProductID { get; set; }
        public Guid BrandID { get; set; }
        public string Address { get; set; }
        public string DeliveryAddress { get; set; }
        public string PhoneNo { get; set; }
        public string IsUpdate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
        public List<MobileRND_Brand_VM> brandList { get; set; }
        public List<MobileRND_Product_VM> productList { get; set; }
        public List<MobileRND_SalesChannel_VM> SalesChannelList { get; set; }
        public List<MobileRND_Zone_VM> ZoneList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.ViewModel.EmployeeInformation
{
    public class MobileRND_EmployeeInformation_VM
    {
        public Guid Id { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string ContactNumber { get; set; }
        //public Guid SalesChannelID { get; set; }
        //public Guid ZoneID { get; set; }
        public string Brand { get; set; }
        public Guid BrandID { get; set; }
        public string EmployeeType { get; set; }
        public string IsUpdate { get; set; }
        //public List<String> AssignedCustomer { get; set; } 

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }

        public List<MobileRND_Brand_VM> brandList { get; set; }
        public List<MobileRND_Product_VM> productList { get; set; }

        //public string SalseChannelName { get; set; }
        //public string ZoneName { get; set; }
        //public List<MobileRND_SalesChannel_VM> SalesChannelList { get; set; }
        //public List<MobileRND_EmployeeInformation_VM> EmployeeList { get; set; }
        //public List<MobileRND_Zone_VM> ZoneList { get; set; }
    }
}

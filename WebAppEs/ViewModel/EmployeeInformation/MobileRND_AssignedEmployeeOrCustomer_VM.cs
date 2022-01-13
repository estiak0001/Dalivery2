using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.ViewModel.EmployeeInformation
{
    public class MobileRND_AssignedEmployeeOrCustomer_VM
    {
        public Guid Id { get; set; }
        public Guid EmployeeInformationID { get; set; }
        public Guid? AssignedEmployeeID { get; set; }
        public Guid? AssignedCustomerID { get; set; }
        public Guid? SalesChannelID { get; set; }
        public Guid? ZoneID { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }


        public string SalseChannelName { get; set; }
        public string ZoneName { get; set; }
        public List<MobileRND_SalesChannel_VM> SalesChannelList { get; set; }
        public List<MobileRND_EmployeeInformation_VM> EmployeeList { get; set; }
        public List<MobileRND_Zone_VM> ZoneList { get; set; }
    }
}

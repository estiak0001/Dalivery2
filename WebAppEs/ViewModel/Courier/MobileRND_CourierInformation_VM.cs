using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.ViewModel.Courier
{
    public class MobileRND_CourierInformation_VM
    {
        public Guid Id { get; set; }
        public string CourierName { get; set; }
        public decimal CoverRate { get; set; }
        public decimal NonCoverRate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RateFixedFromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RateFixedToDate { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
        public string IsUpdate { get; set; }
    }
}

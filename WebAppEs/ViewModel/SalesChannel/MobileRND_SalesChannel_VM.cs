using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.ViewModel.SalesChannel
{
    public class MobileRND_SalesChannel_VM
    {
        public Guid Id { get; set; }
        public string ChannelName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }

        public string IsUpdate { get; set; }
    }
}

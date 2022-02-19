using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.ProductModel;

namespace WebAppEs.ViewModel.Indexing
{
    public class MobileRND_Items_VM
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public string ModelName { get; set; }
        public string ItemNameWithItemCode { get; set; }
        public Guid Brand { get; set; }
        public string BrandName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
        public string IsUpdate { get; set; }
        public List<ProductModel_VM> modelList { get; set; }
        public List<MobileRND_Brand_VM> brandlist { get; set; }
    }
}

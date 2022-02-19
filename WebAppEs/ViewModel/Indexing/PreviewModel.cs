using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.ProductModel;

namespace WebAppEs.ViewModel.Indexing
{
    public class PreviewModel
    {
        public Guid BrandId { get; set; }
        public Guid ModelId { get; set; }
        public Guid ItemId { get; set; }
        public string Grade { get; set; }
        public List<MobileRND_Items_VM> itemlist { get; set; }
        public List<ProductModel_VM> modelList { get; set; }
        public List<MobileRND_Brand_VM> brandlist { get; set; }
    }
}

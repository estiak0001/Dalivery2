using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.ViewModel.Indexing
{
    public class MobileRND_IndexingEntry_VM
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Grade { get; set; }
        public string Block { get; set; }
        public string RackNo { get; set; }
        public string StageNo { get; set; }
        public string ColumNo { get; set; }
        public DateTime? IndexingDate { get; set; }
        public string IndexingDateString { get; set; }
        public string Remarks { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public Guid ModelId { get; set; }
        public string ModelName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public List<MobileRND_Items_VM> itemlist { get; set; }
        public string IsUpdate { get; set; }
    }
}

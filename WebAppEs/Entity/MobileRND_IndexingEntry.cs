using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    public class MobileRND_IndexingEntry
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string Grade { get; set; }

        [Required]
        [StringLength(50)]
        public string Block { get; set; }

        [Required]
        [StringLength(50)]
        public string RackNo { get; set; }

        [Required]
        [StringLength(50)]
        public string StageNo { get; set; }

        [Required]
        [StringLength(50)]
        public string ColumNo { get; set; }

        [Required]
        public DateTime IndexingDate { get; set; }

        public string Remarks { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

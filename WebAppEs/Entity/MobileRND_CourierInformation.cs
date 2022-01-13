using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    public class MobileRND_CourierInformation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string CourierName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CoverRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NonCoverRate { get; set; }

        [Required]
        public DateTime RateFixedFromDate { get; set; }

        [Required]
        public DateTime RateFixedToDate { get; set; }

        public string Address { get; set; }
        public string ContactNumber { get; set; }


        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

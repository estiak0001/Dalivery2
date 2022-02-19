using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_BookingEntry")]
    public class MobileRND_BookingEntry
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(150)]
        public DateTime? BookingDate { get; set; }

        [Required]
        public Guid PaymentTypeId { get; set; }

        [Required]
        public Guid CourierId { get; set; }

        [Required]
        public Guid BrandID { get; set; }

        [Required]
        public Guid ProductID { get; set; }

        //public bool IsApprove { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

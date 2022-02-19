using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_BookingDetailsEntry")]
    public class MobileRND_BookingDetailsEntry
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("MobileRND_BookingEntry")]
        public Guid BookingId { get; set; }
        public virtual MobileRND_BookingEntry MobileRND_BookingEntry { get; set; }
        [Required]
        [StringLength(150)]
        public string CNNumber { get; set; }
        [Required]
        [StringLength(150)]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ammount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }
        [Required]
        [StringLength(150)]
        public string CourierType { get; set; }
        [Required]
        [StringLength(150)]
        public string CustomerNo { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveredDateTime { get; set; }
        public string Remarks { get; set; }
        [StringLength(250)]
        public string DoNo { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
        public bool IsApprove { get; set; }
    }
}

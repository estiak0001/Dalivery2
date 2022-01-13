using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_CustomerInfo")]
    public class MobileRND_CustomerInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string CustomerNo { get; set; }

        [Required]
        [StringLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(150)]
        public Guid SalesChannelID { get; set; }

        [Required]
        [StringLength(150)]
        public Guid ZoneID { get; set; }

        [Required]
        [StringLength(150)]
        public Guid Product { get; set; }

        [Required]
        [StringLength(150)]
        public Guid Brand { get; set; }

        public string Address { get; set; }
        public string DeliveryAddress { get; set; }
        public string PhoneNo { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

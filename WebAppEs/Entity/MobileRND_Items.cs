using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    public class MobileRND_Items
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ModelId { get; set; }
        [Required]
        public Guid Brand { get; set; }
        [StringLength(150)]
        public string ItemCode { get; set; }
        [StringLength(150)]
        public string ItemName { get; set; }
        //[Required]
        //[StringLength(150)]
        //public string Grade { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

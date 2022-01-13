using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_EmployeeInformation")]
    public class MobileRND_EmployeeInformation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string EmployeeID { get; set; }

        [Required]
        [StringLength(250)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(150)]
        public string ContactNumber { get; set; }

        //[Required]
        //[StringLength(150)]
        //public Guid SalesChannelID { get; set; }

        //[Required]
        //[StringLength(150)]
        //public Guid ZoneID { get; set; }

        [Required]
        public Guid Brand { get; set; }

        [Required]
        [StringLength(150)]
        public string EmployeeType { get; set; }

        //[Required]
        //[StringLength(150)]
        //public string AssignedEmployeeID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

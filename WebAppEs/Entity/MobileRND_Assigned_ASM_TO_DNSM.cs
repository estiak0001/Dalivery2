using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_Assigned_ASM_TO_DNSM")]
    public class MobileRND_Assigned_ASM_TO_DNSM
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("MobileRND_EmployeeInformation")]
        public Guid EmployeeDNSMID { get; set; }
        public virtual MobileRND_EmployeeInformation MobileRND_EmployeeInformation { get; set; }
        public Guid? AssignedEmployeeASMID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

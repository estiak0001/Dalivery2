using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEs.Entity
{
    [Table(name: "MobileRND_Assigned_TSO_TO_ASM")]
    public class MobileRND_Assigned_TSO_TO_ASM
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("MobileRND_EmployeeInformation")]
        public Guid EmployeeASMID { get; set; }
        public virtual MobileRND_EmployeeInformation MobileRND_EmployeeInformation { get; set; }
        public Guid? AssignedEmployeeTSOID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public Guid LUser { get; set; }
    }
}

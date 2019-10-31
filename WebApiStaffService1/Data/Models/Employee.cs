using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Interfaces;
using WebApiStaffService1.Data.Models;

namespace WebApiStaffService1.Data.Models
{
    public class Employee : IEntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string IntegretionKey { get; set; }
        public bool State { get; set; }

        public PhysicalPerson PhysicalPerson { get; set; }
        public Guid? PhysicalPersonId { get; set; }

        public OrganizationalUnit OrganizationalUnit { get; set; }
        public Guid? OrganizationalUnitId { get; set; }

        public Position Position { get; set; }
        public Guid? PositionId { get; set; }

    }

}
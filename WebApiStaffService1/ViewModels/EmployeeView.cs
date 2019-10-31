using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStaffService1.ViewModels
{
    public class EmployeeView
    {
        public Guid Id { get; set; }
        //public DateTime CreateOn { get; set; }
        //public DateTime ModifyOn { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string IntegretionKey { get; set; }
        //public string PhysicalPersonName { get; set; }
        public bool State { get; set; }

        public Guid? PhysicalPersonId { get; set; }
        public Guid? OrganizationalUnitId { get; set; }
        public Guid? PositionId { get; set; }

    }
}

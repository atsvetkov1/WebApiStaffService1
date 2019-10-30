using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStaffService1.ViewModels
{
    public class OrganizationalUnitView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public DateTime CreateOn { get; set; }
        //public DateTime ModifyOn { get; set; }
        public string IntegretionKey { get; set; }
        public bool State { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStaffService1.Data.Interfaces
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime CreateOn { get; set; }
        DateTime ModifyOn { get; set; }
        string IntegretionKey { get; set; }
        bool State { get; set; }

    }
}


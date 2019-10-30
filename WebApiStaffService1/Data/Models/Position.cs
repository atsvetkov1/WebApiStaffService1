using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Interfaces;

namespace WebApiStaffService1.Data.Models
{
    public class Position : IEntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public DateTime ModifyOn { get; set; }
        public string IntegretionKey { get; set; }
        public bool State { get; set; }

        //[Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateOn { get; set; }

        public List<Employee> Employees { get; set; }

    }

}
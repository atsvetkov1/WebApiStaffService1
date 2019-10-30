using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Interfaces;

namespace WebApiStaffService1.Data.Models
{
    public class OrganizationalUnit : IEntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public string IntegretionKey { get; set; }
        public bool State { get; set; }

        public List<Employee> Employees { get; set; }

        // ссылка на ID родителя 
        //[Key, ForeignKey("OrganizationalUnitId")]
        //public int? ParentId { get; set; }

        // Уровень подразеделения в общей иерархии
        //public int? UnitLevel { get; set; }

        // ссылка на ID Организации 
        //public Organization Organization { get; set; }

    }
}
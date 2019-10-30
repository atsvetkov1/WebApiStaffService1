using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Interfaces;

namespace WebApiStaffService1.Data.Models
{
    public class PhysicalPerson : IEntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Surname { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        [Column(TypeName = "varchar(2048)")]
        public string PhotoUrl { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public string IntegretionKey { get; set; }
        public bool State { get; set; }

        //Ссылка на сотрудников 1:N
        public List<Employee> Employees { get; set; }


        //public DateTime BirthDate { get; set; }
        //public string Email { get; set; }
        //public string MobilePhoneNumber { get; set; }
        //public int? InternalPhoneNumber { get; set; }
        //public string Address { get; set; }



    }
}
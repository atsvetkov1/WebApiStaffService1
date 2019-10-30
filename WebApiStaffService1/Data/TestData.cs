using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Models;

namespace WebApiStaffService1.Data
{
    public class TestData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EnterpriseStructDbContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any ailments
                if (context.PhysicalPersons != null && context.PhysicalPersons.Any())
                    return;   // DB has already been seeded

                var physicalPersons = GetPhysicalPersons().ToArray();
                context.PhysicalPersons.AddRange(physicalPersons);
                context.SaveChanges();

                var positions = GetPositions().ToArray();
                context.Positions.AddRange(positions);
                context.SaveChanges();

                var organizationalUnits = GetOrganizationalUnits().ToArray();
                context.OrganizationalUnits.AddRange(organizationalUnits);
                context.SaveChanges();

                var employees = GetEmployees(context).ToArray();
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }

        public static List<PhysicalPerson> GetPhysicalPersons()
        {
            List<PhysicalPerson> physicalPersons = new List<PhysicalPerson>() {
                new PhysicalPerson {Id=Guid.NewGuid(), Name="Иван", Surname="Тестовый", Patronymic="Тетович", State=true , IntegretionKey = "1", CreateOn = DateTime.Now, ModifyOn = DateTime.Now },
                new PhysicalPerson {Id=Guid.NewGuid(), Name="Михаил", Surname="Антонов", Patronymic="Краткий", State=true, IntegretionKey = "2", CreateOn = DateTime.Now, ModifyOn = DateTime.Now }
            };
            return physicalPersons;
        }

        public static List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>() {
              new Position {Id=Guid.NewGuid(), Name="Директор", State=true, IntegretionKey = "1", CreateOn = DateTime.Now, ModifyOn = DateTime.Now },
              new Position {Id=Guid.NewGuid(), Name="Секретарь", State=true, IntegretionKey = "2", CreateOn = DateTime.Now, ModifyOn = DateTime.Now }
            };
            return positions;
        }

        public static List<OrganizationalUnit> GetOrganizationalUnits()
        {
            List<OrganizationalUnit> organizationalUnits = new List<OrganizationalUnit>() {
              new OrganizationalUnit {Id=Guid.NewGuid(), Name="Директорат", State=true, IntegretionKey = "1", CreateOn = DateTime.Now, ModifyOn = DateTime.Now },
              new OrganizationalUnit {Id=Guid.NewGuid(), Name="Документооборот", State=true, IntegretionKey = "2", CreateOn = DateTime.Now, ModifyOn = DateTime.Now }
            };
            return organizationalUnits;
        }

        public static List<Employee> GetEmployees(EnterpriseStructDbContext db)
        {

            //var persons = new List<PhysicalPerson>();
            //var personsRes = from t in persons // определяем каждый объект из teams как t
            //                                   //where t.p //фильтрация по критерию
            //                 orderby t  // упорядочиваем по возрастанию
            //                 select t; // выбираем объект


            List<Employee> employees = new List<Employee>();

            for (int i = 0; i <= 1; i++)
            {

                List<PhysicalPerson> physicalPersons = db.PhysicalPersons.Take(0).ToList();

                employees.Add(
                new Employee
                {
                    Id = Guid.NewGuid(),
                    State = true,
                    PhysicalPersonId = (new List<PhysicalPerson>(db.PhysicalPersons))[i].Id,
                    OrganizationalUnitId = db.OrganizationalUnits.ToList()[i].Id,
                    PositionId = db.Positions.ToList()[i].Id,
                    IntegretionKey = "1",
                    CreateOn = DateTime.Now,
                    ModifyOn = DateTime.Now,
                }
                );
            }

            return employees;

        }

        //List<Employee> employees = new List<Employee>() {
        //            new Employee {
        //                Id=Guid.NewGuid(), State = true,
        //                PhysicalPersonId = db.PhysicalPersons.FirstOrDefault().Id,
        //                OrganizationalUnitId = db.OrganizationalUnits.FirstOrDefault().Id,
        //                PositionId = db.Positions.FirstOrDefault().Id
        //            },
        //              new Employee {
        //                Id=Guid.NewGuid(), State = true,
        //                PhysicalPersonId = db.PhysicalPersons.FirstOrDefault().Id,
        //                OrganizationalUnitId = db.OrganizationalUnits.FirstOrDefault().Id,
        //                PositionId = db.Positions.FirstOrDefault().Id
        //              },
        //        };


        //return employees;

        //}
    }
}
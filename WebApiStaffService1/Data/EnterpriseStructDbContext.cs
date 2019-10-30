using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStaffService1.Data.Interfaces;
using WebApiStaffService1.Data.Models;

namespace WebApiStaffService1.Data
{
    public class EnterpriseStructDbContext : DbContext
    {
        public EnterpriseStructDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }
        public DbSet<Position> Positions { get; set; }

        //Не используемы таблицы
        //public DbSet<ManagementUnit> ManagementUnits { get; set; }
        //public DbSet<Organization> Organizations { get; set; }
        //public DbSet<StaffList> StaffList { get; set; }
        //public DbSet<TestEmpoleeView> TestEmpoleeView { get; set; }

        /// <summary>
        /// //настроки одинаковый полей
        /// </summary>
        private void UpdateFieldType<T>(ModelBuilder modelBuilder) where T : class, IEntityBase
        {
            modelBuilder.Entity<T>()
            .Property(b => b.CreateOn) //.HasDefaultValueSql("getdate()")
            .HasColumnType("datetime");

            modelBuilder.Entity<T>()
            .Property(b => b.ModifyOn)  //.HasDefaultValueSql("getdate()")
            .HasColumnType("datetime");

            modelBuilder.Entity<T>()
            .Property(b => b.IntegretionKey)
            .IsRequired()
            .HasColumnType("varchar(100)");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //настроки полей в таблицах:
            UpdateFieldType<Position>(modelBuilder);
            UpdateFieldType<Employee>(modelBuilder);
            UpdateFieldType<OrganizationalUnit>(modelBuilder);
            UpdateFieldType<PhysicalPerson>(modelBuilder);

            #region Закомментированный_код
            //modelBuilder.Entity<Position>()
            //    .Property(b => b.CreateOn)
            //    .HasDefaultValueSql("getdate()")
            //    .HasColumnType("datetime");

            //modelBuilder.Entity<Employee>()
            //   .Property(b => b.CreateOn)
            //   .HasDefaultValueSql("getdate()")
            //   .HasColumnType("datetime");

            //modelBuilder.Entity<OrganizationalUnit>()
            //    .Property(b => b.CreateOn)
            //    .HasDefaultValueSql("getdate()")
            //    .HasColumnType("datetime");

            //modelBuilder.Entity<PhysicalPerson>()
            //    .Property(b => b.CreateOn)
            //    .HasDefaultValueSql("getdate()")
            //    .HasColumnType("datetime");

            // Тип поля ModifyOn
            //modelBuilder.Entity<Position>()
            //    .Property(b => b.ModifyOn)
            //    .HasDefaultValueSql("getdate()")
            //    .HasColumnType("datetime");

            //modelBuilder.Entity<Employee>()
            //   .Property(b => b.ModifyOn)
            //   .HasDefaultValueSql("getdate()")
            //   .HasColumnType("datetime");

            //modelBuilder.Entity<OrganizationalUnit>()
            //  .Property(b => b.ModifyOn)
            //  .HasDefaultValueSql("getdate()")
            //  .HasColumnType("datetime");

            //modelBuilder.Entity<PhysicalPerson>()
            //  .Property(b => b.ModifyOn)
            //  .HasDefaultValueSql("getdate()")
            //  .HasColumnType("datetime");

            //исключаем при миграции
            //modelBuilder.Ignore<TestEmpoleeView>();
            #endregion

        }
    }
}

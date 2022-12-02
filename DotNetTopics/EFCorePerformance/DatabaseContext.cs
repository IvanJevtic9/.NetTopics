using EFCorePerformance.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCorePerformance
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(builder =>
            {
                builder.ToTable("Companies");

                builder
                    .HasMany(company => company.Employees)
                    .WithOne()
                    .HasForeignKey(employee => employee.CompanyId)
                    .IsRequired();

                builder.HasData(new Company
                {
                    Id = 1,
                    Name = "Meta"
                });
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.ToTable("Employees");

                var size = 1000;
                var employees = Enumerable
                    .Range(1, size)
                    .Select(id => new Employee
                    {
                        Id = id,
                        Name = $"Employee #{id}",
                        CompanyId = 1,
                        Salary = 100m
                    })
                    .ToArray();

                builder.HasData(employees);
            });
        }
    }
}

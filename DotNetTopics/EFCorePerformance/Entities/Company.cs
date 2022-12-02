namespace EFCorePerformance.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? LastSalaryUpdateUtc { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}

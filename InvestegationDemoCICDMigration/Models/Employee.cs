using System.ComponentModel.DataAnnotations;

namespace DevOpsWebApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
    }
}

using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.Entities
{
    public class Employee: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string EmployeeName { get; set; }
        [MaxLength(30)]
        public string EmployeePosition {  get; set; }
        [MaxLength(20)]
        [Required]
        public string EmployeePhone {  get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public double Salary { get; set; }

        public List<Order> Orders { get; set; }
    }

}

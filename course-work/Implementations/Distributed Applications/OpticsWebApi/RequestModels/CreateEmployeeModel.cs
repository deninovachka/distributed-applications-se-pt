using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.RequestModels
{
    public class CreateEmployeeModel
    {
        [MaxLength(50)]
        [Required] 
        public string EmployeeName { get; set; }
        [MaxLength(30)]
        [Required]
        public string EmployeePosition { get; set; }
        [MaxLength(20)]
        [Required]
        public string EmployeePhone { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        public double Salary {  get; set; } 
    }
}

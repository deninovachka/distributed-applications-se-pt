using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.RequestModels
{
    public class CreateDoctorModel
    {
        [MaxLength(50)]
        [Required]
        public string DoctorName { get; set; }
        [MaxLength(40)]
        [Required]
        public string DoctorSpecialization { get; set; }
        [MaxLength(20)]
        [Required]
        public string DoctorPhone { get; set; }
        [MaxLength(100)]
        [Required]
        public string DoctorEmail { get; set; }
        public double DoctorFee {  get; set; }
    }
}

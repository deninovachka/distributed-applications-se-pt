using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.Entities
{
    public class Doctor: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string DoctorName { get; set; }
        [MaxLength(40)]
        public string DoctorSpecialization { get; set;}
        [MaxLength(20)]
        [Required]
        public string DoctorPhone {  get; set;}
        [MaxLength(100)]
        public string DoctorEmail {  get; set; }
        public double DoctorFee { get; set; }

        public List<Prescription> Prescriptions { get; set; }
    }

}

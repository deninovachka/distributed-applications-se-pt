using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.Entities
{
    public class Prescription: BaseEntity
    {
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public DateTime PrescriptionDate { get; set; }
        [MaxLength(100)]
        [Required]
        public string GlassesType { get; set; }
        public decimal SphOD { get; set; }
        public decimal CylOD { get; set; }
        public decimal AxOD { get; set; }
        public decimal SphOS { get; set; }
        public decimal CylOS { get; set; }
        public decimal AxOS { get; set; }
        public decimal PD { get; set; }
    }
}

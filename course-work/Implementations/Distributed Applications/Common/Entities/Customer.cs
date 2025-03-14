using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.Entities
{
    public class Customer: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string CustomerName { get; set; }
        [MaxLength(100)]
        public string CustomerEmail { get; set; }
        [MaxLength(20)]
        [Required]
        public string CustomerPhone { get; set; }
        [MaxLength(100)]
        public string CustomerAddress {  get; set; }

        public bool IsFemale {  get; set; }
        public List<Order> Orders { get; set; }
        public List<Prescription> Prescriptions { get; set; }

    }
    
}

using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.RequestModels
{
    public class CreateCustomerModel
    {
        [MaxLength(50)]
        [Required]
        public string CustomerName { get; set; }
        [MaxLength(100)]
        [Required]
        public string CustomerEmail { get; set; }
        [MaxLength(20)]
        [Required]
        public string CustomerPhone { get; set; }
        [MaxLength(100)]
        [Required]
        public string CustomerAddress { get; set; }
        public bool IsFemale {  get; set; }
    }
}

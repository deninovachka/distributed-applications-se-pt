using Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.Entities
{
    public class Product: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Brand { get; set; }
        [MaxLength(50)]
        [Required]
        public string Model {  get; set; }
        [MaxLength(50)]
        [Required]
        public string Color {  get; set; }
        [MaxLength(50)]
        public string Category {  get; set; }
        public decimal Price {  get; set; }
        public int Quantity {  get; set; }

        
    }
}

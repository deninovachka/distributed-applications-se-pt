using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.RequestModels
{
    public class CreateProductModel
    {
        [MaxLength(50)]
        [Required]
        public string Brand { get; set; }
        [MaxLength(50)]
        [Required]
        public string Model { get; set; }
        [MaxLength(50)]
        [Required]
        public string Color { get; set; }
        [MaxLength(50)]
        [Required]
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

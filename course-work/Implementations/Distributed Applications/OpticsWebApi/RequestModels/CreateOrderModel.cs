using OpticsWebApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpticsWebApi.RequestModels
{
    public class CreateOrderModel
    {
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }

        public decimal TotalAmount { get; set; }
        [MaxLength(100)]
        [Required]
        public string ShippingAddress { get; set; }
    }
}

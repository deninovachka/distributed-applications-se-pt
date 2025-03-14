using Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticsWebApi.Entities
{
    public class Order: BaseEntity
    {

       
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public decimal TotalAmount { get; set; }
        [MaxLength(100)]
        [Required]
        public string ShippingAddress { get; set; }
       
        public List<OrderDetails> OrderDetails { get; set; }= new List<OrderDetails>(); 

    }

}

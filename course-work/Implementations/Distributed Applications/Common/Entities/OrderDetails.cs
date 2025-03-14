using Common.Entities;

namespace OpticsWebApi.Entities
{
    public class OrderDetails: BaseEntity
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public bool IsShipped { get; set; }
    }
}

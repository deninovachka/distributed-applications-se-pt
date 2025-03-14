using OpticsWebApi.Entities;

namespace OpticsWebApi.RequestModels
{
    public class CreateOrderDetailsModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsShipped {  get; set; }

    }
}

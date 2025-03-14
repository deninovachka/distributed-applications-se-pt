using OpticsWebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticsWebApi.ResultModels
{
    public class GetOrderModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public decimal TotalAmount { get; set; }
      
    }
}

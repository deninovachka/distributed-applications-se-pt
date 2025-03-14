﻿using OpticsWebApi.Entities;

namespace OpticsWebApi.ResultModels
{
    public class GetOrderDetailsModel
    {
        public int OrderDetailsID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsShipped {  get; set; }
    }
}

namespace OpticsWebApi.ResultModels
{
    public class GetProductModel
    {
        public int ProductID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

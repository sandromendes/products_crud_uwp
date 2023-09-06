namespace ProductsCRUD.Models.Products
{
    public class ProductFilterRequest
    {
        public string ProductName { get; set; }
        public double ProductMinValue { get; set; } = 0;
        public double ProductMaxValue { get; set; } = 0;

        public ProductFilterRequest(string productName, double productMinValue, double productMaxValue)
        {
            ProductName = productName;
            ProductMinValue = productMinValue;
            ProductMaxValue = productMaxValue;
        }
    }
}

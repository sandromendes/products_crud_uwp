using Windows.UI.Xaml.Media.Imaging;

namespace ProductsCRUD.Models.Products
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public BitmapImage Image { get; set; }
        public byte[] ByteImage { get; set; }
    }
}

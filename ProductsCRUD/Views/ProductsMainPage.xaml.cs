using ProductsCRUD.Models.Products;
using ProductsCRUD.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class ProductsMainPage : Page
    {
        public ProductsMainPageViewModel ViewModel => (ProductsMainPageViewModel)DataContext;
        public ProductsMainPage()
        {
            this.InitializeComponent();
        }

        public void DeleteCommand(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (ProductDto)button.Tag;

            ViewModel.DeleteCommand(item.Id);
        }

        public void EditCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.EditProductCommand(sender, e);
        }
    }
}

using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class ProductAddPage : Page
    {
        public ProductAddPageViewModel ViewModel => (ProductAddPageViewModel)DataContext;
        public ProductAddPage()
        {
            this.InitializeComponent();
        }
    }
}

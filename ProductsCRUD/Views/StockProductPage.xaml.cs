using ProductsCRUD.Models.Products;
using ProductsCRUD.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductsCRUD.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class StockProductPage : Page
    {
        public StockProductPageViewModel ViewModel => (StockProductPageViewModel)DataContext;
        public StockProductPage()
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
            ViewModel.EditCommand(sender, e);
        }
    }
}

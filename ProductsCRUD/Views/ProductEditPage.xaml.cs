using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;
using ProductsCRUD.Services.Images;
using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ProductsCRUD.Services.Products;
using ProductsCRUD.Models.Products;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductsCRUD.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class ProductEditPage : Page
    {
        public ProductEditViewModel ViewModel { get; }
        public ProductEditPage()
        {
            this.InitializeComponent();

            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var productService = ServiceLocator.Current.GetInstance<IProductService>();
            var imageConversionService = ServiceLocator.Current.GetInstance<IImageConversionService>();

            ViewModel = new ProductEditViewModel(productService, navigationService, imageConversionService);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ProductDto product)
            {
                ViewModel.UpdatedProduct = product;
                ViewModel.ImageSource = product.Image;
            }
            base.OnNavigatedTo(e);
        }
    }
}

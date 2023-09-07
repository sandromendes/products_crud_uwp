using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;
using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ProductsCRUD.Business.Services.Products;
using ProductsCRUD.Business.Services.Images;
using ProductsCRUD.Business.Models.Products;

namespace ProductsCRUD.Views
{
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

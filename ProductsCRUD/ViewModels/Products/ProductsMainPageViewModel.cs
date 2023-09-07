using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ProductsCRUD.Domain.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Messages.Products;
using ProductsCRUD.Common.Util.Labels;
using ProductsCRUD.Business.Services.Images;
using ProductsCRUD.Business.Services.Products;
using ProductsCRUD.Business.Models.Products;

namespace ProductsCRUD.ViewModels
{
    public class ProductsMainPageViewModel : ViewModelBase
    {
        private readonly string PRODUCT_IMAGE_NOT_ADDED = "ms-appx:///Assets/LockScreenLogo.scale-200.png";
        private readonly string PRODUCT_TEMP_IMAGE_NAME = "TemporaryImageForProduct";

        private string filterProductName;
        public string FilterProductName
        {
            get { return filterProductName; }
            set { SetProperty(ref filterProductName, value); }
        }

        private double filterProductMinValue;
        public double FilterProductMinValue
        {
            get { return filterProductMinValue; }
            set { SetProperty(ref filterProductMinValue, value); }
        }

        private double filterProductMaxValue;
        public double FilterProductMaxValue
        {
            get { return filterProductMaxValue; }
            set { SetProperty(ref filterProductMaxValue, value); }
        }

        public ObservableCollection<ProductDto> Products { get; }

        private readonly INavigationService navigationService;
        private readonly IImageConversionService imageConversionService;
        private readonly IProductService productService;

        public ProductsMainPageViewModel(IProductService productService,
            INavigationService navigationService,
            IImageConversionService imageConversionService)
        {
            Products = new ObservableCollection<ProductDto>();
            this.productService = productService;
            this.navigationService = navigationService;
            this.imageConversionService = imageConversionService;
            LoadProducts(Task.Run(() => productService.GetProducts()).Result);
        }

        public void NewProductCommand(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate(PageTokens.ProductsPage.NEW, null);
        }

        public void EditProductCommand(object sender, RoutedEventArgs e) 
        {
            var button = (Button)sender;
            var updatedProduct = (ProductDto)button.Tag;

            navigationService.Navigate(PageTokens.ProductsPage.EDIT, updatedProduct);
        }

        public async void DeleteCommand(string id) 
        {
            var confirmDialog = new ContentDialog
            {
                Title = "Confirmar Remoção",
                Content = "O produto será removido. Deseja continuar?",
                PrimaryButtonText = "Sim",
                CloseButtonText = "Não"
            };

            var result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await productService.DeleteProduct(id);
                ShowRemoveMessage();
                LoadProducts(await productService.GetProducts());
            }
            else
            {
                ShowCanceledMessage();
            }
        }

        public void FilterProducts()
        {
            var request = new ProductFilterRequest(filterProductName, filterProductMinValue, filterProductMaxValue);

            var filteredProducts = productService.GetProductsByFilter(request);

            LoadProducts(filteredProducts);
        }

        private async void LoadProducts(IList<ProductDto> products)
        {
            var emptyProductFile = await StorageFile
                .GetFileFromApplicationUriAsync(new Uri(PRODUCT_IMAGE_NOT_ADDED));

            var emptyProductImage = await imageConversionService.ConvertFileToBitmapImage(emptyProductFile);

            Products.Clear();
            foreach (var product in products)
            {
                BitmapImage tempImage;

                if (product.ByteImage == null)
                    tempImage = emptyProductImage;
                else
                {
                    var storageFile = await imageConversionService.ConvertBytesToStorageFile(product.ByteImage, PRODUCT_TEMP_IMAGE_NAME);
                    tempImage = await imageConversionService.ConvertFileToBitmapImage(storageFile);
                }

                product.Image = tempImage;
                Products.Add(product);
            }
        }

        private async void ShowRemoveMessage()
        {
            var dialog = new ContentDialog
            {
                Title = ProductMessages.PRODUCT_REMOVED_TITLE,
                Content = ProductMessages.PRODUCT_REMOVED_CONTENT,
                
                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }

        private async void ShowCanceledMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Não executado",
                Content = "Ação cancelada pelo usuário",

                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}

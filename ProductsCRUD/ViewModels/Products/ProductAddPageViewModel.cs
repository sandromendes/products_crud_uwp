using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Business.Models.Products;
using ProductsCRUD.Business.Services.Images;
using ProductsCRUD.Business.Services.Products;
using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Labels;
using ProductsCRUD.Common.Util.Messages.Images;
using ProductsCRUD.Common.Util.Messages.Products;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ProductsCRUD.ViewModels
{
    public class ProductAddPageViewModel : ViewModelBase
    {
        private StorageFile _imageFile;

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private string _productDescription;
        public string ProductDescription
        {
            get { return _productDescription; }
            set { SetProperty(ref _productDescription, value); }
        }

        private double _productPrice;
        public double ProductPrice
        {
            get { return _productPrice; }
            set { SetProperty(ref _productPrice, value); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private readonly IProductService productService;
        private readonly INavigationService navigationService;
        private readonly IImageConversionService imageConversionService;

        public ProductAddPageViewModel(IProductService productService,
            INavigationService navigationService,
            IImageConversionService imageConversionService)
        {
            this.productService = productService;
            this.navigationService = navigationService;
            this.imageConversionService = imageConversionService;
        }

        public async void SaveProduct()
        {
            try
            {
                imageConversionService.ValidateImage(_imageFile);

                var product = new ProductDto
                {
                    Name = ProductName,
                    Description = ProductDescription,
                    Price = ProductPrice,
                    ByteImage = _imageFile != null ? await imageConversionService.ConvertStorageFileToByteArray(_imageFile) : null
                };

                await productService.AddProduct(product);
                ShowAddedProductMessage();
                navigationService.Navigate(PageTokens.ProductsPage.MAIN, null);
            }
            catch (InvalidImageException)
            {
                ShowInvalidImageMessage();
                ImageSource = null;
                return;
            }

            ProductName = string.Empty;
            ProductDescription = string.Empty;
            ProductPrice = 0;
            ImageSource = null;
        }

        public async void SelectImage()
        {
            var imageFile = await imageConversionService.PickImage();

            if (imageFile != null)
            {
                var bitmapImage = await imageConversionService.ConvertFileToBitmapImage(imageFile);
                ImageSource = bitmapImage;
            };

            _imageFile = imageFile;
        }

        private async void ShowInvalidImageMessage()
        {
            var dialog = new ContentDialog
            {
                Title = ImageMessages.INVALID_IMAGE_TITLE,
                Content = ImageMessages.INVALID_IMAGE_CONTENT,
                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }

        private async void ShowAddedProductMessage()
        {
            var dialog = new ContentDialog
            {
                Title = ProductMessages.PRODUCT_CREATED_TITLE,
                Content = ProductMessages.PRODUCT_CREATED_CONTENT,
                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}

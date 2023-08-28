using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models.Products;
using ProductsCRUD.Services.Images;
using ProductsCRUD.Services.Products;
using ProductsCRUD.Util;
using ProductsCRUD.Util.Labels;
using ProductsCRUD.Util.Messages.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ProductsCRUD.ViewModels
{
    public class ProductEditViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ProductDto _updatedProduct;
        public ProductDto UpdatedProduct
        {
            get { return _updatedProduct; }
            set { SetProperty(ref _updatedProduct, value); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private readonly INavigationService navigationService;
        private readonly IImageConversionService imageConversionService;
        private readonly IProductService productService;

        public ProductEditViewModel(IProductService productService,
            INavigationService navigationService,
            IImageConversionService imageConversionService)
        {
            UpdatedProduct = new ProductDto(); // Crie um novo produto ao inicializar a ViewModel
            this.productService = productService;
            this.navigationService = navigationService;
            this.imageConversionService = imageConversionService;
        }

        public void UpdateProduct()
        {
            var product = new Product
            {
                Id = UpdatedProduct.Id,
                Name = UpdatedProduct.Name,
                Description = UpdatedProduct.Description,
                Price = UpdatedProduct.Price,
                Image = UpdatedProduct.ByteImage,
            };

            productService.UpdateProduct(product);
            ShowSaveMessage();
            navigationService.Navigate(PageTokens.PRODUCT_MAIN, null);
        }

        public void CancelEdition()
        {
            navigationService.GoBack();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (e.Parameter is ProductDto product)
            {
                UpdatedProduct = product;
                ImageSource = product.Image;
            }
        }

        public async void SelectImage()
        {
            var imageFile = await imageConversionService.PickImage();

            if (imageFile != null)
            {
                var bitmapImage = await imageConversionService.ConvertFileToBitmapImage(imageFile);
                ImageSource = bitmapImage;
                UpdatedProduct.ByteImage = await imageConversionService.ConvertStorageFileToByteArray(imageFile);
            };
        }

        private async void ShowSaveMessage()
        {
            var dialog = new ContentDialog
            {
                Title = ProductMessages.PRODUCT_UPDATED_TITLE,
                Content = string.Format(ProductMessages.PRODUCT_UPDATED_CONTENT, UpdatedProduct.Name),
                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}

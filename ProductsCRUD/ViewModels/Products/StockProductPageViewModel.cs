using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Util;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ProductsCRUD.Exceptions;
using ProductsCRUD.Util.Labels;
using ProductsCRUD.Util.Messages.Product;
using ProductsCRUD.Services.Images;
using ProductsCRUD.Util.Messages.Images;
using ProductsCRUD.Models.Products;
using ProductsCRUD.Services.Products;
using System.Windows.Input;

namespace ProductsCRUD.ViewModels
{
    public class StockProductPageViewModel : ViewModelBase
    {
        private readonly string PRODUCT_IMAGE_NOT_ADDED = "ms-appx:///Assets/LockScreenLogo.scale-200.png";
        private readonly string PRODUCT_TEMP_IMAGE_NAME = "TemporaryImageForProduct";

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

        public ObservableCollection<ProductDto> Products { get; }

        private readonly INavigationService navigationService;
        private readonly IImageConversionService imageConversionService;
        private readonly IProductService productService;

        public StockProductPageViewModel(IProductService productService,
            INavigationService navigationService,
            IImageConversionService imageConversionService)
        {
            Products = new ObservableCollection<ProductDto>();
            this.productService = productService;
            this.navigationService = navigationService;
            this.imageConversionService = imageConversionService;
            LoadProducts();
        }

        public StockProductPageViewModel()
        {
        }

        public void EditCommand(object sender, RoutedEventArgs e) 
        {
            var button = (Button)sender;
            var updatedProduct = (ProductDto)button.Tag;

            navigationService.Navigate(PageTokens.PRODUCT_EDITION, updatedProduct);
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
                productService.DeleteProduct(id);
                ShowRemoveMessage();
                LoadProducts();
            }
            else
            {
                ShowCanceledMessage();
            }
        }

        public async void SaveProduct()
        {
            try
            {
                imageConversionService.ValidateImage(_imageFile);

                var product = new Product
                {
                    Name = ProductName,
                    Description = ProductDescription,
                    Price = ProductPrice,
                    Image = await imageConversionService.ConvertStorageFileToByteArray(_imageFile)
                };

                // Chama o serviço para salvar o produto no banco de dados SQLite
                productService.AddProduct(product);

                Products.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,  
                    Image = await imageConversionService.ConvertFileToBitmapImage(_imageFile)
                });

                ShowAddedProductMessage();
            }
            catch (InvalidImageException)
            {
                ShowInvalidImageMessage();
                ImageSource = null;
                return;
            }

            // Limpa os campos após salvar o produto
            ProductName = string.Empty;
            ProductDescription = string.Empty;
            ProductPrice = 0;
            ImageSource = null;
        }

        private async void LoadProducts()
        {
            // Chama o serviço para obter a lista de produtos cadastrados no banco de dados SQLite
            var products = productService.GetProducts();

            var emptyProductFile = await StorageFile
                .GetFileFromApplicationUriAsync(new Uri(PRODUCT_IMAGE_NOT_ADDED));

            var emptyProductImage = await imageConversionService.ConvertFileToBitmapImage(emptyProductFile);

            Products.Clear();
            foreach (var product in products)
            {
                BitmapImage tempImage;

                if (product.Image == null)
                    tempImage = emptyProductImage;
                else
                {
                    var storageFile = await imageConversionService.ConvertBytesToStorageFile(product.Image, PRODUCT_TEMP_IMAGE_NAME);
                    tempImage = await imageConversionService.ConvertFileToBitmapImage(storageFile);
                }

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = tempImage,
                    ByteImage = product.Image
                };

                Products.Add(productDto);
            }
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

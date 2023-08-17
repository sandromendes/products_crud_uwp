using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models;
using ProductsCRUD.Services;
using ProductsCRUD.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using ProductsCRUD.Exceptions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;

namespace ProductsCRUD.ViewModels
{
    public class StockProductPageViewModel : ViewModelBase
    {
        private StorageFile _imageFile;

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }

        private string productDescription;
        public string ProductDescription
        {
            get { return productDescription; }
            set { SetProperty(ref productDescription, value); }
        }

        private double productPrice;
        public double ProductPrice
        {
            get { return productPrice; }
            set { SetProperty(ref productPrice, value); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ObservableCollection<ProductDto> Products { get; }

        public DelegateCommand SaveProductCommand { get; set; }

        private readonly INavigationService navigationService;

        private readonly IProductService productService;

        public StockProductPageViewModel(IProductService productService,
            INavigationService navigationService)
        {
            Products = new ObservableCollection<ProductDto>();
            this.productService = productService;
            this.navigationService = navigationService;
            LoadProducts();
            SaveProductCommand = new DelegateCommand(SaveProduct);
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

        public void DeleteCommand(int id) 
        {
            productService.DeleteProduct(id);
            ShowRemoveMessage();
            LoadProducts();
        }

        public async void SaveProduct()
        {
            try
            {
                ValidateImage();

                var product = new Product
                {
                    Name = ProductName,
                    Description = ProductDescription,
                    Price = ProductPrice,
                    Image = await ConvertStorageFileToByteArray(_imageFile)
                };

                // Chama o serviço para salvar o produto no banco de dados SQLite
                productService.AddProduct(product);

                Products.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,  
                    Image = await ConvertFileToBitmapImage(_imageFile)
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
                .GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/LockScreenLogo.scale-200.png"));

            var emptyProductImage = await ConvertFileToBitmapImage(emptyProductFile);

            Products.Clear();
            foreach (var product in products)
            {
                BitmapImage tempImage;

                if (product.Image == null)
                    tempImage = emptyProductImage;
                else
                    tempImage = await ConvertFileToBitmapImage(await ConvertBytesToStorageFile(product.Image));

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
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var imageFile = await picker.PickSingleFileAsync();

            if (imageFile != null)
            {
                var bitmapImage = await ConvertFileToBitmapImage(imageFile);
                ImageSource = bitmapImage;
            };

            _imageFile = imageFile;
        }

        private async void ShowInvalidImageMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Imagem inválida",
                Content = $"Selecione outra imagem!",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        private async void ShowRemoveMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Produto Removido",
                Content = $"Produto removido com sucesso!",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        private async void ShowAddedProductMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Produto Adicionado",
                Content = $"Produto adicionado com sucesso!",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        private async Task<byte[]> ConvertStorageFileToByteArray(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();
                byte[] buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private async Task<StorageFile> ConvertBytesToStorageFile(byte[] bytes)
        {
            StorageFile tempFile = await ApplicationData
                .Current
                .TemporaryFolder
                .CreateFileAsync("TemporaryImageForProduct", CreationCollisionOption.GenerateUniqueName);

            using (IRandomAccessStream stream = await tempFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter writer = new DataWriter(stream))
                {
                    writer.WriteBytes(bytes);
                    await writer.StoreAsync();
                }
            }

            return tempFile;
        }

        private async void ValidateImage()
        {
            var byteImage = await ConvertStorageFileToByteArray(_imageFile);

            if (byteImage == null) return;

            if (byteImage.Length <= 1024 * 1024) // 1 MB
                return;

            throw new InvalidImageException();
        }

        private async Task<BitmapImage> ConvertFileToBitmapImage(StorageFile file)
        {
            var bitmapImage = new BitmapImage();
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await bitmapImage.SetSourceAsync(stream);
            }
            return bitmapImage;
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models;
using ProductsCRUD.Services;
using ProductsCRUD.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ProductsCRUD.ViewModels
{
    public class ProductEditViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ProductDto updatedProduct;

        public ProductDto UpdatedProduct
        {
            get { return updatedProduct; }
            set { SetProperty(ref updatedProduct, value); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private readonly INavigationService navigationService;

        private readonly IProductService productService;
        public ProductEditViewModel(IProductService productService,
            INavigationService navigationService)
        {
            UpdatedProduct = new ProductDto(); // Crie um novo produto ao inicializar a ViewModel
            this.productService = productService;
            this.navigationService = navigationService;
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

        private async void ShowSaveMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Produto Atualizado",
                Content = $"Produto \"{UpdatedProduct.Name}\" atualizado com sucesso!",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
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
                UpdatedProduct.ByteImage = await ConvertStorageFileToByteArray(imageFile);
            };
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
    }
}

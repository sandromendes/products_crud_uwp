﻿using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Util;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ProductsCRUD.Util.Labels;
using ProductsCRUD.Services.Images;
using ProductsCRUD.Models.Products;
using ProductsCRUD.Services.Products;
using ProductsCRUD.Util.Messages.Products;

namespace ProductsCRUD.ViewModels
{
    public class ProductsMainPageViewModel : ViewModelBase
    {
        private readonly string PRODUCT_IMAGE_NOT_ADDED = "ms-appx:///Assets/LockScreenLogo.scale-200.png";
        private readonly string PRODUCT_TEMP_IMAGE_NAME = "TemporaryImageForProduct";

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
            LoadProducts();
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
                productService.DeleteProduct(id);
                ShowRemoveMessage();
                LoadProducts();
            }
            else
            {
                ShowCanceledMessage();
            }
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
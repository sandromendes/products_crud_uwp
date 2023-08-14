using CommunityToolkit.Mvvm.Input;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models;
using ProductsCRUD.Services;
using ProductsCRUD.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

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
                //Image = UpdatedProduct.Image
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
            }
        }
    }
}

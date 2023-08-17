using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;
using ProductsCRUD.Models;
using ProductsCRUD.Services;
using ProductsCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

            ViewModel = new ProductEditViewModel(productService, navigationService);
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

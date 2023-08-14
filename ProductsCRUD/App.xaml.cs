using Prism.Unity.Windows;
using ProductsCRUD.Repositories;
using ProductsCRUD.Services;
using ProductsCRUD.Util;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace ProductsCRUD
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = new AppShell();
            shell.SetFrame(rootFrame);

            return shell;
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(PageTokens.PRODUCT_MAIN, null);

            return Task.CompletedTask;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(IProductService), typeof(ProductService), true);
            RegisterTypeIfMissing(typeof(IProductRepository), typeof(ProductRepository), true);
        }
    }
}

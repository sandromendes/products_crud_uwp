using ProductsCRUD.Util;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using ProductsCRUD.Services.Images;
using ProductsCRUD.Services.Users;
using ProductsCRUD.Repositories.Users;
using ProductsCRUD.DbContext;
using ProductsCRUD.Services.Products;
using ProductsCRUD.Repositories.Products;
using ProductsCRUD.Services.Token;
using ProductsCRUD.Services;
using ProductsCRUD.Infra.Session;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;
using ProductsCRUD.Services.Navigation;
using Microsoft.Practices.ServiceLocation;

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
            var navigationManager = ServiceLocator.Current.GetInstance<INavigationManager>();
            
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            userService.CreateSuperUserIfDoesntExists();

            if(navigationManager.IsValidAuth())
                NavigationService.Navigate(PageTokens.HomePage.MAIN, null);

            return Task.CompletedTask;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(IProductService), typeof(ProductService), true);
            RegisterTypeIfMissing(typeof(IUserService), typeof(UserService), true);
            RegisterTypeIfMissing(typeof(IImageConversionService), typeof(ImageConversionService), true);
            RegisterTypeIfMissing(typeof(ITokenService), typeof(TokenService), true);
            RegisterTypeIfMissing(typeof(IAuthManagerService), typeof(AuthManagerService), true);
            RegisterTypeIfMissing(typeof(INavigationManager), typeof(NavigationManager), true);
            
            RegisterTypeIfMissing(typeof(IProductRepository), typeof(ProductRepository), true);
            RegisterTypeIfMissing(typeof(IUserRepository), typeof(UserRepository), true);

            var dbContext = new AppDbContext();
            Container.RegisterInstance(dbContext);

            var sessionCache = new SessionCache();
            Container.RegisterInstance(sessionCache);
        }
    }
}

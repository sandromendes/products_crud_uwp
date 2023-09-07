using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;
using ProductsCRUD.Business.Services.Navigation;
using ProductsCRUD.Common.Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD
{
    public sealed partial class AppShell : UserControl
    {
        private readonly INavigationService _navigationService;
        private readonly INavigationManager _navigationManager;

        public AppShell()
        {
            this.InitializeComponent();
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            _navigationManager = ServiceLocator.Current.GetInstance<INavigationManager>();
        }

        public void SetFrame(Frame frame)
        {
            FrameContainer.Content = frame;
        }

        public void ProfileClickAction(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(PageTokens.UsersPage.USER_PROFILE, null);
        }

        public void NotificationsClickAction(object sender, RoutedEventArgs e)
        { 
            _navigationService.Navigate(PageTokens.UsersPage.USER_NOTIFICATIONS, null);
        }

        public void LogoffClickAction(object sender, RoutedEventArgs e) 
        {
            _navigationManager.Logoff();
        }
    }
}

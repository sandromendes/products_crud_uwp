using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class UserNotificationsPage : Page
    {
        public UserNotificationsPageViewModel ViewModel => (UserNotificationsPageViewModel)DataContext;

        public UserNotificationsPage()
        {
            this.InitializeComponent();
        }
    }
}
